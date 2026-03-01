using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace Calculator.Forms
{
    /// <summary>
    /// Логика взаимодействия для Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        private ModelVisual3D roomVisual;
        private ModelVisual3D speakerVisual;
        private Point3D speakerPosition;
        private bool isDragging = false;
        bool isLoaded = false;
        private TranslateTransform3D speakerTransform;
        private double speakerHeight = 0.3; // Высота динамика

        public Test()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CreateSpeaker();
            InitializeRoom();
            SetupDragHandling();

            // Центрируем камеру на комнате
            Viewport3D.ZoomExtents();
            isLoaded = true;
        }

        private void InitializeRoom()
        {
            if (roomVisual != null)
                Viewport3D.Children.Remove(roomVisual);

            roomVisual = new ModelVisual3D();
            var modelGroup = new Model3DGroup();

            double length = double.Parse(RoomLength.Text);
            double width = double.Parse(RoomWidth.Text);
            double height = double.Parse(RoomHeight.Text);

            // Материал для стен (чуть более видимый)
            var wallMaterial = new DiffuseMaterial(new SolidColorBrush(Color.FromArgb(60, 150, 150, 150)));

            // ПОЛ - делаем очень прозрачным и с двусторонним рендерингом
            var floor = CreateBox(new Point3D(0, -height / 2, 0), length, 0.05, width, wallMaterial);
            modelGroup.Children.Add(floor);

            // Потолок - убираем временно
             modelGroup.Children.Add(CreateBox(new Point3D(0, height / 2, 0), length, 0.05, width, wallMaterial));

            // Задняя стена (-Z)
            var backWall = CreateBox(new Point3D(0, 0, -width / 2), length, height, 0.05, wallMaterial);
            backWall.BackMaterial = wallMaterial;
            modelGroup.Children.Add(backWall);

            // Передняя стена (+Z)
            var frontWall = CreateBox(new Point3D(0, 0, width / 2), length, height, 0.05, wallMaterial);
            frontWall.BackMaterial = wallMaterial;
            modelGroup.Children.Add(frontWall);

            // Левая стена (-X)
            var leftWall = CreateBox(new Point3D(-length / 2, 0, 0), 0.05, height, width, wallMaterial);
            leftWall.BackMaterial = wallMaterial;
            modelGroup.Children.Add(leftWall);

            // Правая стена (+X)
            var rightWall = CreateBox(new Point3D(length / 2, 0, 0), 0.05, height, width, wallMaterial);
            rightWall.BackMaterial = wallMaterial;
            modelGroup.Children.Add(rightWall);

            roomVisual.Content = modelGroup;
            Viewport3D.Children.Add(roomVisual);

            // Добавляем сетку ПОД полом для ориентации
            var grid = new GridLinesVisual3D
            {
                Width = length,
                Length = width,
                MinorDistance = 1,
                MajorDistance = 5,
                Thickness = 1
            };
            grid.Transform = new TranslateTransform3D(0, -height / 2 - 0.1, 0); // Сетка под полом
            //Viewport3D.Children.Add(grid);

            // Добавляем координатные оси для ориентации
            var axes = new CoordinateSystemVisual3D
            {
                ArrowLengths = 1
            };
            Viewport3D.Children.Add(axes);
        }

        private GeometryModel3D CreateBox(Point3D center, double xLength, double yLength, double zLength, Material material)
        {
            var mesh = new MeshGeometry3D();

            double x = xLength / 2;
            double y = yLength / 2;
            double z = zLength / 2;

            Point3D p0 = new Point3D(center.X - x, center.Y - y, center.Z - z);
            Point3D p1 = new Point3D(center.X + x, center.Y - y, center.Z - z);
            Point3D p2 = new Point3D(center.X + x, center.Y + y, center.Z - z);
            Point3D p3 = new Point3D(center.X - x, center.Y + y, center.Z - z);
            Point3D p4 = new Point3D(center.X - x, center.Y - y, center.Z + z);
            Point3D p5 = new Point3D(center.X + x, center.Y - y, center.Z + z);
            Point3D p6 = new Point3D(center.X + x, center.Y + y, center.Z + z);
            Point3D p7 = new Point3D(center.X - x, center.Y + y, center.Z + z);

            // Передняя грань (Z+)
            AddTriangle(mesh, p4, p5, p6);
            AddTriangle(mesh, p4, p6, p7);

            // Задняя грань (Z-)
            AddTriangle(mesh, p1, p0, p3);
            AddTriangle(mesh, p1, p3, p2);

            // Левая грань (X-)
            AddTriangle(mesh, p0, p4, p7);
            AddTriangle(mesh, p0, p7, p3);

            // Правая грань (X+)
            AddTriangle(mesh, p5, p1, p2);
            AddTriangle(mesh, p5, p2, p6);

            // Верхняя грань (Y+)
            AddTriangle(mesh, p3, p7, p6);
            AddTriangle(mesh, p3, p6, p2);

            // Нижняя грань (Y-)
            AddTriangle(mesh, p1, p5, p4);
            AddTriangle(mesh, p1, p4, p0);

            return new GeometryModel3D
            {
                Geometry = mesh,
                Material = material,
                BackMaterial = material
            };
        }

        private void AddTriangle(MeshGeometry3D mesh, Point3D p1, Point3D p2, Point3D p3)
        {
            int index = mesh.Positions.Count;
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.Positions.Add(p3);

            Vector3D normal = Vector3D.CrossProduct(p2 - p1, p3 - p1);
            normal.Normalize();

            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);

            mesh.TextureCoordinates.Add(new Point(0, 0));
            mesh.TextureCoordinates.Add(new Point(1, 0));
            mesh.TextureCoordinates.Add(new Point(1, 1));

            mesh.TriangleIndices.Add(index);
            mesh.TriangleIndices.Add(index + 1);
            mesh.TriangleIndices.Add(index + 2);
        }

        private void CreateSpeaker()
        {
            if (speakerVisual != null)
                Viewport3D.Children.Remove(speakerVisual);

            speakerVisual = new ModelVisual3D();

            // Параметры динамика
            double width = 0.4;
            double depth = 0.3;
            speakerHeight = 0.5;

            var mesh = new MeshGeometry3D();

            double x = width / 2;
            double y = speakerHeight / 2;
            double z = depth / 2;

            // Центрируем динамик по центру его объема
            Point3D center = new Point3D(0, 0, 0);

            Point3D p0 = new Point3D(center.X - x, center.Y - y, center.Z - z);
            Point3D p1 = new Point3D(center.X + x, center.Y - y, center.Z - z);
            Point3D p2 = new Point3D(center.X + x, center.Y + y, center.Z - z);
            Point3D p3 = new Point3D(center.X - x, center.Y + y, center.Z - z);
            Point3D p4 = new Point3D(center.X - x, center.Y - y, center.Z + z);
            Point3D p5 = new Point3D(center.X + x, center.Y - y, center.Z + z);
            Point3D p6 = new Point3D(center.X + x, center.Y + y, center.Z + z);
            Point3D p7 = new Point3D(center.X - x, center.Y + y, center.Z + z);

            // Передняя грань (Z+)
            AddTriangle(mesh, p4, p5, p6);
            AddTriangle(mesh, p4, p6, p7);

            // Задняя грань (Z-)
            AddTriangle(mesh, p1, p0, p3);
            AddTriangle(mesh, p1, p3, p2);

            // Левая грань (X-)
            AddTriangle(mesh, p0, p4, p7);
            AddTriangle(mesh, p0, p7, p3);

            // Правая грань (X+)
            AddTriangle(mesh, p5, p1, p2);
            AddTriangle(mesh, p5, p2, p6);

            // Верхняя грань (Y+)
            AddTriangle(mesh, p3, p7, p6);
            AddTriangle(mesh, p3, p6, p2);

            // Нижняя грань (Y-)
            AddTriangle(mesh, p1, p5, p4);
            AddTriangle(mesh, p1, p4, p0);

            var speakerModel = new GeometryModel3D
            {
                Geometry = mesh,
                Material = new DiffuseMaterial(Brushes.Red),
                BackMaterial = new DiffuseMaterial(Brushes.Red)
            };

            // Добавляем черную окантовку для лучшей видимости
            var edgeMaterial = new DiffuseMaterial(Brushes.Black);
            var edgeModel = new GeometryModel3D
            {
                Geometry = CreateEdges(center, width, speakerHeight, depth),
                Material = edgeMaterial
            };

            var group = new Model3DGroup();
            group.Children.Add(speakerModel);
            group.Children.Add(edgeModel);

            speakerTransform = new TranslateTransform3D();
            group.Transform = speakerTransform;

            speakerVisual.Content = group;
            Viewport3D.Children.Add(speakerVisual);

            // Устанавливаем начальную позицию в центре комнаты на полу
            double roomHeight = double.Parse(RoomHeight.Text);
            double roomLength = double.Parse(RoomLength.Text);
            double roomWidth = double.Parse(RoomWidth.Text);

            speakerPosition = new Point3D(0, 0, roomWidth / 2);
            UpdateSpeakerPosition();
        }

        private Geometry3D CreateEdges(Point3D center, double width, double height, double depth)
        {
            var mesh = new MeshGeometry3D();
            double x = width / 2;
            double y = height / 2;
            double z = depth / 2;

            Point3D[] corners = new Point3D[]
            {
                new Point3D(center.X - x, center.Y - y, center.Z - z),
                new Point3D(center.X + x, center.Y - y, center.Z - z),
                new Point3D(center.X + x, center.Y + y, center.Z - z),
                new Point3D(center.X - x, center.Y + y, center.Z - z),
                new Point3D(center.X - x, center.Y - y, center.Z + z),
                new Point3D(center.X + x, center.Y - y, center.Z + z),
                new Point3D(center.X + x, center.Y + y, center.Z + z),
                new Point3D(center.X - x, center.Y + y, center.Z + z)
            };

            int[][] edges = new int[][]
            {
                new int[] {0,1}, new int[] {1,2}, new int[] {2,3}, new int[] {3,0},
                new int[] {4,5}, new int[] {5,6}, new int[] {6,7}, new int[] {7,4},
                new int[] {0,4}, new int[] {1,5}, new int[] {2,6}, new int[] {3,7}
            };

            foreach (var edge in edges)
            {
                AddLine(mesh, corners[edge[0]], corners[edge[1]]);
            }

            return mesh;
        }

        private void AddLine(MeshGeometry3D mesh, Point3D p1, Point3D p2)
        {
            // Рисуем линию как тонкий цилиндр (упрощенно - используем треугольники)
            Vector3D direction = p2 - p1;
            double length = direction.Length;
            direction.Normalize();

            // Создаем перпендикулярный вектор
            Vector3D perp = new Vector3D(1, 0, 0);
            if (Math.Abs(Vector3D.DotProduct(direction, perp)) > 0.99)
                perp = new Vector3D(0, 1, 0);

            Vector3D v1 = Vector3D.CrossProduct(direction, perp);
            v1.Normalize();
            Vector3D v2 = Vector3D.CrossProduct(v1, direction);
            v2.Normalize();

            double radius = 0.02; // Толщина линии

            Point3D center1 = p1;
            Point3D center2 = p2;

            // Создаем 8 точек вокруг каждого центра
            for (int i = 0; i < 8; i++)
            {
                double angle = i * Math.PI / 4;
                Point3D offset1 = center1 + v1 * radius * Math.Cos(angle) + v2 * radius * Math.Sin(angle);
                Point3D offset2 = center2 + v1 * radius * Math.Cos(angle) + v2 * radius * Math.Sin(angle);

                mesh.Positions.Add(offset1);
                mesh.Positions.Add(offset2);
            }

            // Соединяем точки треугольниками
            for (int i = 0; i < 8; i++)
            {
                int next = (i + 1) % 8;
                int baseIdx = i * 2;
                int nextIdx = next * 2;

                // Первый треугольник
                mesh.TriangleIndices.Add(baseIdx);
                mesh.TriangleIndices.Add(baseIdx + 1);
                mesh.TriangleIndices.Add(nextIdx + 1);

                // Второй треугольник
                mesh.TriangleIndices.Add(baseIdx);
                mesh.TriangleIndices.Add(nextIdx + 1);
                mesh.TriangleIndices.Add(nextIdx);
            }
        }

        private void SetupDragHandling()
        {
            Viewport3D.MouseLeftButtonDown += (s, e) =>
            {
                var position = e.GetPosition(Viewport3D);
                var hit = Viewport3D.Viewport.FindNearestPoint(position);

                if (hit.HasValue)
                {
                    isDragging = true;
                    Viewport3D.Cursor = Cursors.Hand;
                    e.Handled = true;
                }
            };

            Viewport3D.MouseMove += (s, e) =>
            {
                if (isDragging)
                {
                    var position = e.GetPosition(Viewport3D);
                    var hit = Viewport3D.Viewport.FindNearestPoint(position);

                    if (hit.HasValue)
                    {
                        double length = double.Parse(RoomLength.Text) / 2;
                        double width = double.Parse(RoomWidth.Text) / 2;
                        double height = double.Parse(RoomHeight.Text) / 2;

                        double x = Math.Max(-length, Math.Min(length, hit.Value.X));
                        double z = Math.Max(-width, Math.Min(width, hit.Value.Z));
                        double y = Math.Max(-height, Math.Min(height, hit.Value.Y));

                        // Позволяем двигать и по Y (вверх/вниз) с ограничениями
                        //double y = Math.Max(-height + speakerHeight / 2, Math.Min(height - speakerHeight / 2, hit.Value.Y));

                        speakerPosition = new Point3D(x, y, z);
                        UpdateSpeakerPosition();
                    }
                }
            };

            Viewport3D.MouseLeftButtonUp += (s, e) =>
            {
                isDragging = false;
                Viewport3D.Cursor = null;
            };

            Viewport3D.MouseLeave += (s, e) =>
            {
                isDragging = false;
                Viewport3D.Cursor = null;
            };
        }

        private void UpdateSpeakerPosition()
        {
            if (speakerTransform != null)
            {
                speakerTransform.OffsetX = speakerPosition.X;
                speakerTransform.OffsetY = speakerPosition.Y;
                speakerTransform.OffsetZ = speakerPosition.Z;

                SpeakerX.Text = speakerPosition.X.ToString("F2");
                SpeakerY.Text = speakerPosition.Y.ToString("F2");
                SpeakerZ.Text = speakerPosition.Z.ToString("F2");
            }
        }

        private void RoomParameter_Changed(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                InitializeRoom();

                // Пересоздаем динамик, чтобы он правильно позиционировался
                CreateSpeaker();
                Viewport3D.ZoomExtents();
            }
        }

        private void SpeakerParameter_Changed(object sender, RoutedEventArgs e)
        {
            if (isLoaded && double.TryParse(SpeakerX.Text, out double x) &&
                double.TryParse(SpeakerY.Text, out double y) &&
                double.TryParse(SpeakerZ.Text, out double z))
            {
                double length = double.Parse(RoomLength.Text) / 2;
                double width = double.Parse(RoomWidth.Text) / 2;
                double height = double.Parse(RoomHeight.Text) / 2;

                x = Math.Max(-length, Math.Min(length, x));
                z = Math.Max(-width, Math.Min(width, z));
                y = Math.Max(-height + speakerHeight / 2, Math.Min(height - speakerHeight / 2, y));

                speakerPosition = new Point3D(x, y, z);
                UpdateSpeakerPosition();
            }
        }

        private void AddSpeaker_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция добавления нескольких динамиков будет добавлена позже", "Информация");
        }
    }
}

using System.Collections;
using System.Drawing;

namespace LiftingCrane.Filter
{
    public class AnEngine
    {
        // размеры изображения
        private int picture_size_x, picture_size_y;

        // положение полос прокрутки, будет использовано в будующем
        private int scroll_x, scroll_y;

        // размер оконной части (объекта AnT)
        private int screen_width, screen_height;

        // номер активного слоя
        private int ActiveLayerNom;

        // массив слоев
        private ArrayList Layers = new ArrayList();


        // последний установленный цвет
        private Color LastColorInUse;

        // конструктор класса
        public AnEngine(int size_x, int size_y, int screen_w, int screen_h)
        {
            // при инициализации экземпляра класса сохраним настройки
            // размеров элементов и изображения в локальных переменных

            picture_size_x = size_x;
            picture_size_y = size_y;

            screen_width = screen_w;
            screen_height = screen_h;

            // полосы прокрутки у нас пока отсутствуют, поэтому просто обнулим значение переменных
            scroll_x = 0;
            scroll_y = 0;

            // добавим новый слой для работы, пока что он будет единственным
            Layers.Add(new AnLayer(picture_size_x, picture_size_y));

            // номер активного слоя - 0
            ActiveLayerNom = 0;
        }

        public void Filter_4()
        {
            // собираем матрицу
            // для данного фильтра нам необзодимо будет произвести два преобразования

            float[] mat = new float[9];

            mat[0] = 0.50f;
            mat[1] = 1.0f;
            mat[2] = 0.50f;
            mat[3] = 1.0f;
            mat[4] = 2.0f;
            mat[5] = 1.0f;
            mat[6] = 0.50f;
            mat[7] = 1.0f;
            mat[8] = 0.50f;

            //вызываем функцию обработки, передавая туда матрицу и дополнительные параметры
            ((AnLayer)Layers[ActiveLayerNom]).PixelTransformation(mat, 0, 2, true);

            mat[0] = -0.5f;
            mat[1] = -0.5f;
            mat[2] = -0.5f;
            mat[3] = -0.5f;
            mat[4] = 6.0f;
            mat[5] = -0.5f;
            mat[6] = -0.5f;
            mat[7] = -0.5f;
            mat[8] = -0.5f;

            //вызываем функцию обработки, передавая туда матрицу и дополнительные параметры
            ((AnLayer)Layers[ActiveLayerNom]).PixelTransformation(mat, 0, 1, false);
        }

        public void SwapImage()
        {
            // вызываем функцию визуализации в нашем слое
            for (int ax = 0; ax < Layers.Count; ax++)
            {
                // эсли данный слой является активным в данный момент
                if (ax == ActiveLayerNom)
                {
                    // вызываем визуализацию данного слоя напрямую
                    ((AnLayer)Layers[ax]).RenderImage(false);
                }
                else
                {
                    // вызываем визуализацию слоя из дисплейного списка
                    ((AnLayer)Layers[ax]).RenderImage(true);
                }
            }
        }

    }
}

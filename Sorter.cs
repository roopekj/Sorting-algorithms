using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Sorting_algorithms
{
    public partial class Sorter : Form
    {
        private Random random = new Random();
        private List<int[]> values = new List<int[]>();
        private int frame_index = 0;
        private static Timer timer = new Timer();

        public Sorter()
        {
            InitializeComponent();
            refresh_values_list();
            timer = new Timer();
            timer.Tick += new EventHandler(new_frame);
            timer.Interval = 1;
            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0;
            chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0;
        }

        private void selection_sort(int[] array)
        {
            int size = array.Length;
            refresh_values_list();
            for (int index = 0; index < size; index++)
            {
                values.Add(array.ToArray());
                int smallest_index = index;
                for (int x = smallest_index + 1; x < array.Length; x++)
                {
                    if (array[x] < array[smallest_index])
                    {
                        smallest_index = x;
                    }
                }
                var val = array[index];
                array[index] = array[smallest_index];
                array[smallest_index] = val;
            }
        }

        public void double_selection_sort(int[] array)  // Deciding the smallest and biggest value in one scan through the array
        {
            refresh_values_list();
            int size = array.Length;
            int i = 0;
            int j = size - 1;
            while (true)
            {
                int smallest_index = i;
                int biggest_index = j;
                for (int index = i + 1; index <= j; index++)
                {
                    if (array[index] < array[smallest_index])
                    {
                        smallest_index = index;
                    }
                    if (array[index] > array[biggest_index])
                    {
                        biggest_index = index;
                    }
                }

                var val = array[i];
                array[i] = array[smallest_index];
                array[smallest_index] = val;
                values.Add(array.ToArray());

                if (values.Count == size - 1)
                {
                    break;
                }
                else if (array[smallest_index] > array[biggest_index])
                {
                    biggest_index = smallest_index;
                }
                i++;

                var val_ = array[j];
                array[j] = array[biggest_index];
                array[biggest_index] = val_;
                values.Add(array.ToArray());

                if (values.Count == size - 1)
                {
                    break;
                }
                j--;
            }
        }

        public void shaker_sort(int[] array)    //i.e. two-way bubble sort
        {
            refresh_values_list();
            int size = array.Length;
            while (true)
            {
                for (int index = 0; index < array.Length - 1; index++)
                {
                    if (array[index] > array[index + 1])
                    {
                        var s = array[index + 1];
                        array[index + 1] = array[index];
                        array[index] = s;
                    }
                }
                values.Add(array.ToArray());

                if (values.Count == size - 1)
                {
                    break;
                }
                for (int index_ = array.Length - 1; index_ > 0; index_--)
                {
                    if (array[index_] < array[index_ - 1])
                    {
                        var s = array[index_ - 1];
                        array[index_ - 1] = array[index_];
                        array[index_] = s;
                    }
                }
                values.Add(array.ToArray());

                if (values.Count == size - 1)
                {
                    break;
                }
            }
        }

        private void bubble_sort(int[] array)
        {
            refresh_values_list();
            for (int index = array.Length - 1; index > 0; index--)
            {
                for (int x = 0; x < index; x++)
                {
                    if (array[x + 1] < array[x])
                    {
                        var val = array[x + 1];
                        array[x + 1] = array[x];
                        array[x] = val;
                        values.Add(array.ToArray());
                    }
                }
            }
        }

        private void gnome_sort(int[] array)
        {
            refresh_values_list();
            for (int index = 1; index < array.Length; index++)
            {
                for (int x = index - 1; x >= -1; x--)
                {
                    if (x == -1)
                    {
                        int value = array[index];
                        for (int shift_index = index; shift_index > 0; shift_index--)
                        {
                            array[shift_index] = array[shift_index - 1];
                            values.Add(array.ToArray());
                        }
                        array[0] = value;
                        break;
                    }
                    else if (array[x] < array[index])
                    {
                        int value = array[index];
                        for (int shift_index = index; shift_index > x + 1; shift_index--)
                        {
                            array[shift_index] = array[shift_index - 1];
                            values.Add(array.ToArray());
                        }
                        array[x + 1] = value;
                        values.Add(array.ToArray());
                        break;
                    }
                }
            }
        }

        private void insertion_sort(int[] array)
        {
            refresh_values_list();
            for (int index = 1; index < array.Length; index++)
            {
                for (int x = 0; x < index; x++)
                {
                    if (array[x] > array[index])
                    {
                        int value = array[index];
                        for (int shift_index = index; shift_index > x; shift_index--)
                        {
                            array[shift_index] = array[shift_index - 1];
                            values.Add(array.ToArray());
                        }
                        array[x] = value;
                        values.Add(array.ToArray());
                    }
                }
            }
        }

        private int sort_range(int start, int end, int[] array)  // Moves all smaller values to the left and bigger values to the right of array[end]. Returns final index of original pivot.
        {
            int i = start;
            int pivot = array[end];
            for (int index = start; index < end; index++)
            {
                if (array[index] < pivot)
                {
                    var val = array[index];
                    array[index] = array[i];
                    array[i] = val;
                    i++;
                    values.Add(array.ToArray());
                }
            }

            array[end] = array[i];
            array[i] = pivot;

            return i;
        }
        private void quick_sort(int[] array)
        {
            refresh_values_list();
            pivot(0, array.Length - 1, array);
        }
        private void pivot(int start, int end, int[] array)
        {
            if (start < end)    // Recursively sorts values to the left of the original array[end] until sort_range returns a value equal to start
                                // At this point the smallest value has been moved to the beginning of the given array and there are no values left to sort in the first section
                                // Lastly sorts values to the right of the original array[end]
            {
                int i = sort_range(start, end, array);
                pivot(start, i - 1, array);
                pivot(i, end, array);
            }
        }

        private void new_data() // Pushing new data-points into the chart
        {
            int[] data;
            try
            {
                data = values[frame_index];
                int val = data[0];
            }
            catch
            {
                return;
            }
            chart1.Series["Numbers"].Points.Clear();
            int i = 0;
            foreach (int value in data)
            {
                chart1.Series["Numbers"].Points.AddXY(i, value);
                i++;
            }
            chart1.Update();
        }
        public void new_frame(Object sender, EventArgs e)   // Moves the animation forward by one frame
        {
            timer.Stop();
            new_data();
            frame_index++;
            timer.Enabled = true;
        }
        private int[] randomized_array(int[] array, Random random)  // Returns the given array in a random order
        {
            return array.OrderBy(i => random.Next()).ToArray();
        }
        private void refresh_values_list()   // Making the code a bit more readable
        {
            values = new List<int[]>();
        }
        private void initiate() // A few lines of boilerplate that need to run regardless of the sorting algorithm used
        {
            frame_index = 0;
            timer.Start();
            Application.DoEvents();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selection_sort(randomized_array(Enumerable.Range(1, 100).ToArray(), random));
            initiate();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            bubble_sort(randomized_array(Enumerable.Range(1, 100).ToArray(), random));
            initiate();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            insertion_sort(randomized_array(Enumerable.Range(1, 100).ToArray(), random));
            initiate();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            quick_sort(randomized_array(Enumerable.Range(1, 100).ToArray(), random));
            initiate();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            shaker_sort(randomized_array(Enumerable.Range(1, 100).ToArray(), random));
            initiate();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            gnome_sort(randomized_array(Enumerable.Range(1, 100).ToArray(), random));
            initiate();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            double_selection_sort(randomized_array(Enumerable.Range(1, 100).ToArray(), random));
            initiate();
        }
    }
}
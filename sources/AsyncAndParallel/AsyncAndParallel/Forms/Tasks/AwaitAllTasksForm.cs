﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncAndParallel.Forms.Tasks
{
    public partial class AwaitAllTasksForm : BaseListBoxProgressForm
    {
        public AwaitAllTasksForm()
        {
            InitializeComponent();
        }

        protected override async Task OnStartAsync()
        {
            StartProgressBar();

            Stopwatch sw = Stopwatch.StartNew();

            List<Task> tasks = new List<Task>();
            for (int i = 0; i < Number; i++)
            {
                var task = StartRandomTaskAsync(1000, 3000);
                listBoxResult.Items.Add($"Started task {task.Id}.");
                tasks.Add(task);
            }
            
            // awaits for all tasks to finish.
            await Task.WhenAll(tasks);

            sw.Stop();
            string taskIds = "";
            tasks.ForEach((t) => taskIds += t.Id + " ");

            listBoxResult.Items.Add($"All tasks ({taskIds}) finished in {sw.ElapsedMilliseconds} milliseconds.");

            StopProgressBar();
        }
    }
}

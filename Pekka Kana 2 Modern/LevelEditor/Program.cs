using System;

namespace LevelEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            MapEditor form = new MapEditor();
            form.Show();
            form.game = new Game1(
                form.pctSurface.Handle,
                form,
                form.pctSurface);
            form.game.Run();


        }
    }
}


using Controller;
using System.Collections.Generic;
using System;

namespace View
{
    public class Program
    {
        static void Main(string[] args){
            MainMenu menu = new MainMenu(new AppController(new Model.AppRepo(new Model.Entities.revtrainingdbContext())));
            menu.Start();
        }
    }
}
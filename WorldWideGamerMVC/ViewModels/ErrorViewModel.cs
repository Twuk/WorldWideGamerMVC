using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldWideGamerMVC.ViewModels
{
    public class ErrorViewModel
    {
        public string Tekst { get; set; }
        public dynamic ViewModel { get; set; }

        public ErrorViewModel()
        {

        }

        public ErrorViewModel(string tekst)
        {
            Tekst = tekst;
        }

        public ErrorViewModel(string tekst, dynamic viewModel)
        {
            Tekst = tekst;
            ViewModel = viewModel;
        }
    }
}
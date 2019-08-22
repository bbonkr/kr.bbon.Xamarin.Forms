using System;
using System.Collections.Generic;
using System.Text;

namespace kr.bbon.Xamarin.Forms
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RouteAttribute : Attribute
    {
        public RouteAttribute(string path)
        {
            this.path = path;
        }

        public string Path { get => path; }

        private readonly string path;
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Imagen


    {

        public int Id { get; set; }
        public int IdProducto { get; set; }
        public string ImagenUrl { get; set; }
        public bool Estado { get; set; }
        public Imagen() { }
        public Imagen(string img)
        {
            ImagenUrl = img;
        }
        public override string ToString()
        {
            return ImagenUrl;
        }




    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaglikApplication.Models
{
    public class YeniRandevuClass
    {
        public List<Hasta> hastas { get; set; }

        public List<Doktor> doktors { get; set;}

        public List<Poliklinik> polikliniks { get; set; }

        public List<Randevu> randevus { get; set; }

    }
}
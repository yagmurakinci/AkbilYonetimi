using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkbilYonetimiEntityLayer.Entities
{
    class Akbil:IKayitTarihiProperty
    {
        public string AkbilNo { get; set; }
        public decimal Bakiye { get; set; }
        public DateTime SonKullanimTarihi { get; set; }
        public int AkbilSahibiID { get; set; }
        public AkbilTipleri AkbilTipi { get; set; }
        public DateTime KayitTarihi { get ; set ; }

        //Akbil tipi gelecek
    }
}

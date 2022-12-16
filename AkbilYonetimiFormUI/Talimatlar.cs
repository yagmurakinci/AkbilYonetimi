using AkbilYonetimiEntityLayer.Entities;
using System;

namespace AkbilYonetimiFormUI
{
    internal class Talimatlar
    {
        public object OlusturulmaTarihi { get; internal set; }
        public object AkbilID { get; internal set; }
        public object YuklenecekMiktar { get; internal set; }
        public object YuklendiMi { get; internal set; }
        public object YuklendigiTarihi { get; internal set; }

        public static implicit operator Talimatlar(Talimat v)
        {
            throw new NotImplementedException();
        }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GastroPages
{
    using System;
    using System.Collections.Generic;
    
    public partial class Benutzer
    {
        public int id { get; set; }
        public string Nachname { get; set; }
        public string Vorname { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public bool IstAdmin { get; set; }
        public bool IstAktiv { get; set; }
        public string Passwort { get; set; }
    }
}

using GastroPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class AdminAllergeneModel
    {
        
        public List<Allergene> AlleAllergene { get; set; }
        public Allergene AllergenItem { get; set; }

        public AdminAllergeneModel()
        {
            AlleAllergene = new List<Allergene>();
            using (GastroEntities _db = new GastroEntities())
            {
                AlleAllergene = (from Allergene al in _db.Allergene select al).ToList();
                AllergenItem = new Allergene();
                AllergenItem.id = 0;
            }
        }

        public AdminAllergeneModel(int id)
        {
            AlleAllergene = new List<Allergene>();
            using (GastroEntities _db = new GastroEntities())
            {
                AlleAllergene = (from Allergene al in _db.Allergene select al).ToList();
                AllergenItem = AlleAllergene.Where(x => x.id == id).FirstOrDefault();
            }
        }
    }
}
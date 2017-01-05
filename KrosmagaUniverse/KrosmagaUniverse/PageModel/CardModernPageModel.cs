using KrosmagaUniverse.Models;
using PropertyChanged;
using System.Collections.Generic;
using FreshMvvm;
using KrosmagaUniverse.KrosData;

namespace KrosmagaUniverse.PageModel
{
    [ImplementPropertyChanged]
    class CardModernPageModel : FreshBasePageModel
    {
        private CardModernModel _selectedItem;
        
        public IEnumerable<KrosCard> CardsModern { get; set; }

        public CardModernPageModel()
        {
            ControleurGererDataAccess _controleurGererDataAccess = new ControleurGererDataAccess();
            _controleurGererDataAccess.FillTheDb();
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            var list = new List<KrosCard>();

            list = new List<KrosCard>();

            //Charge toute les cartes
            //list = App.DAUtil.GetAllCards();
            //Charge les cartes par classes 
            list = App.DAUtil.GetAllCardsByClass((int)EnumHelper.ClasseKrosmaga.Ecaflip);
            #region list.add
            //list.Add(new CardModernModel { PA = 3, PM = 4, PV = 5, ATQ = 4, Name = "Betty Boubz", Description = "zzz", ImagePath = "http://i.imgur.com/vHqvLC1.png" });
            //list.Add(new CardModernModel { PA = 3, PM = 4, PV = 5, ATQ = 4, Name = "clara_byne", Description = "", ImagePath = "http://i.imgur.com/TXiPWLk.png" });
            //list.Add(new CardModernModel { PA = 5, PM = 3, PV = 7, ATQ = 4, Name = "criblage", Description = "", ImagePath = "http://i.imgur.com/EnE5zKy.png" });
            //list.Add(new CardModernModel { PA = 7, PM = 5, PV = 4, ATQ = 4, Name = "dernier_recours", Description = "", ImagePath = "http://i.imgur.com/TtMIE9q.png" });
            //list.Add(new CardModernModel { PA = 4, PM = 6, PV = 8, ATQ = 4, Name = "detournement", Description = "", ImagePath = "http://i.imgur.com/sxqFVNm.png" });
            //list.Add(new CardModernModel { PA = 8, PM = 2, PV = 2, ATQ = 4, Name = "eksa_soth", Description = "Asparagus pee stink.", ImagePath = "http://i.imgur.com/jNuv8Pw.png" });
            //list.Add(new CardModernModel { PA = 2, PM = 4, PV = 3, ATQ = 4, Name = "esquive", Description = "Broccoli donice with fruit.", ImagePath = "http://i.imgur.com/ILrODPc.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 3, PV = 2, ATQ = 4, Name = "fantome_cra", Description = "Avocadoing taste better.", ImagePath = "http://i.imgur.com/mwcJSpl.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 5, PV = 4, ATQ = 4, Name = "fleche_chercheuse", Description = "Tvomit.", ImagePath = "http://i.imgur.com/pxKZ7m8.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 6, PV = 3, ATQ = 4, Name = "fleche_criblante", Description = "Ases your pee stink.", ImagePath = "http://i.imgur.com/YlnfZWR.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 1, PV = 5, ATQ = 4, Name = "fleche_d_immolation", Description = "esn’t play nice with fruit.", ImagePath = "http://i.imgur.com/qBlsMef.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 3, PV = 6, ATQ = 4, Name = "fleche_de_recul", Description = "Avonothing taste better.", ImagePath = "http://i.imgur.com/WntZk0A.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 2, PV = 2, ATQ = 4, Name = "fleche_destructrice", Description = "e vomit.", ImagePath = "http://i.imgur.com/Dz7mMOr.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 4, PV = 4, ATQ = 4, Name = "fleche_explosive", Description = "Ases your pee stink.", ImagePath = "http://i.imgur.com/W1Rj2hC.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 3, PV = 3, ATQ = 4, Name = "fleche_percante", Description = "Bro’t play nice with fruit.", ImagePath = "http://i.imgur.com/x0Y6Auw.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 5, PV = 5, ATQ = 4, Name = "fleche_tempete", Description = "Avocothing taste better.", ImagePath = "http://i.imgur.com/8LTtHwP.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 6, PV = 6, ATQ = 4, Name = "guy_yomtella", Description = "Tastes.", ImagePath = "http://i.imgur.com/zMIeHjK.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 2, PV = 3, ATQ = 4, Name = "harcelement", Description = "Asparagur pee stink.", ImagePath = "http://i.imgur.com/mWtemyH.png" });
            //list.Add(new CardModernModel { PA = 3, PM = 4, PV = 5, ATQ = 4, Name = "Betty Boubz", Description = "zzz", ImagePath = "http://i.imgur.com/vHqvLC1.png" });
            //list.Add(new CardModernModel { PA = 3, PM = 4, PV = 5, ATQ = 4, Name = "clara_byne", Description = "", ImagePath = "http://i.imgur.com/TXiPWLk.png" });
            //list.Add(new CardModernModel { PA = 5, PM = 3, PV = 7, ATQ = 4, Name = "criblage", Description = "", ImagePath = "http://i.imgur.com/EnE5zKy.png" });
            //list.Add(new CardModernModel { PA = 7, PM = 5, PV = 4, ATQ = 4, Name = "dernier_recours", Description = "", ImagePath = "http://i.imgur.com/TtMIE9q.png" });
            //list.Add(new CardModernModel { PA = 4, PM = 6, PV = 8, ATQ = 4, Name = "detournement", Description = "", ImagePath = "http://i.imgur.com/sxqFVNm.png" });
            //list.Add(new CardModernModel { PA = 8, PM = 2, PV = 2, ATQ = 4, Name = "eksa_soth", Description = "Asparagus pee stink.", ImagePath = "http://i.imgur.com/jNuv8Pw.png" });
            //list.Add(new CardModernModel { PA = 2, PM = 4, PV = 3, ATQ = 4, Name = "esquive", Description = "Broccoli donice with fruit.", ImagePath = "http://i.imgur.com/ILrODPc.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 3, PV = 2, ATQ = 4, Name = "fantome_cra", Description = "Avocadoing taste better.", ImagePath = "http://i.imgur.com/mwcJSpl.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 5, PV = 4, ATQ = 4, Name = "fleche_chercheuse", Description = "Tvomit.", ImagePath = "http://i.imgur.com/pxKZ7m8.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 6, PV = 3, ATQ = 4, Name = "fleche_criblante", Description = "Ases your pee stink.", ImagePath = "http://i.imgur.com/YlnfZWR.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 1, PV = 5, ATQ = 4, Name = "fleche_d_immolation", Description = "esn’t play nice with fruit.", ImagePath = "http://i.imgur.com/qBlsMef.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 3, PV = 6, ATQ = 4, Name = "fleche_de_recul", Description = "Avonothing taste better.", ImagePath = "http://i.imgur.com/WntZk0A.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 2, PV = 2, ATQ = 4, Name = "fleche_destructrice", Description = "e vomit.", ImagePath = "http://i.imgur.com/Dz7mMOr.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 4, PV = 4, ATQ = 4, Name = "fleche_explosive", Description = "Ases your pee stink.", ImagePath = "http://i.imgur.com/W1Rj2hC.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 3, PV = 3, ATQ = 4, Name = "fleche_percante", Description = "Bro’t play nice with fruit.", ImagePath = "http://i.imgur.com/x0Y6Auw.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 5, PV = 5, ATQ = 4, Name = "fleche_tempete", Description = "Avocothing taste better.", ImagePath = "http://i.imgur.com/8LTtHwP.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 6, PV = 6, ATQ = 4, Name = "guy_yomtella", Description = "Tastes.", ImagePath = "http://i.imgur.com/zMIeHjK.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 2, PV = 3, ATQ = 4, Name = "harcelement", Description = "Asparagur pee stink.", ImagePath = "http://i.imgur.com/mWtemyH.png" });
            //list.Add(new CardModernModel { PA = 3, PM = 4, PV = 5, ATQ = 4, Name = "Betty Boubz", Description = "zzz", ImagePath = "http://i.imgur.com/vHqvLC1.png" });
            //list.Add(new CardModernModel { PA = 3, PM = 4, PV = 5, ATQ = 4, Name = "clara_byne", Description = "", ImagePath = "http://i.imgur.com/TXiPWLk.png" });
            //list.Add(new CardModernModel { PA = 5, PM = 3, PV = 7, ATQ = 4, Name = "criblage", Description = "", ImagePath = "http://i.imgur.com/EnE5zKy.png" });
            //list.Add(new CardModernModel { PA = 7, PM = 5, PV = 4, ATQ = 4, Name = "dernier_recours", Description = "", ImagePath = "http://i.imgur.com/TtMIE9q.png" });
            //list.Add(new CardModernModel { PA = 4, PM = 6, PV = 8, ATQ = 4, Name = "detournement", Description = "", ImagePath = "http://i.imgur.com/sxqFVNm.png" });
            //list.Add(new CardModernModel { PA = 8, PM = 2, PV = 2, ATQ = 4, Name = "eksa_soth", Description = "Asparagus pee stink.", ImagePath = "http://i.imgur.com/jNuv8Pw.png" });
            //list.Add(new CardModernModel { PA = 2, PM = 4, PV = 3, ATQ = 4, Name = "esquive", Description = "Broccoli donice with fruit.", ImagePath = "http://i.imgur.com/ILrODPc.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 3, PV = 2, ATQ = 4, Name = "fantome_cra", Description = "Avocadoing taste better.", ImagePath = "http://i.imgur.com/mwcJSpl.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 5, PV = 4, ATQ = 4, Name = "fleche_chercheuse", Description = "Tvomit.", ImagePath = "http://i.imgur.com/pxKZ7m8.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 6, PV = 3, ATQ = 4, Name = "fleche_criblante", Description = "Ases your pee stink.", ImagePath = "http://i.imgur.com/YlnfZWR.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 1, PV = 5, ATQ = 4, Name = "fleche_d_immolation", Description = "esn’t play nice with fruit.", ImagePath = "http://i.imgur.com/qBlsMef.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 3, PV = 6, ATQ = 4, Name = "fleche_de_recul", Description = "Avonothing taste better.", ImagePath = "http://i.imgur.com/WntZk0A.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 2, PV = 2, ATQ = 4, Name = "fleche_destructrice", Description = "e vomit.", ImagePath = "http://i.imgur.com/Dz7mMOr.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 4, PV = 4, ATQ = 4, Name = "fleche_explosive", Description = "Ases your pee stink.", ImagePath = "http://i.imgur.com/W1Rj2hC.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 3, PV = 3, ATQ = 4, Name = "fleche_percante", Description = "Bro’t play nice with fruit.", ImagePath = "http://i.imgur.com/x0Y6Auw.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 5, PV = 5, ATQ = 4, Name = "fleche_tempete", Description = "Avocothing taste better.", ImagePath = "http://i.imgur.com/8LTtHwP.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 6, PV = 6, ATQ = 4, Name = "guy_yomtella", Description = "Tastes.", ImagePath = "http://i.imgur.com/zMIeHjK.png" });
            //list.Add(new CardModernModel { PA = 1, PM = 2, PV = 3, ATQ = 4, Name = "harcelement", Description = "Asparagur pee stink.", ImagePath = "http://i.imgur.com/mWtemyH.png" });
            #endregion

            CardsModern = list;
            
        }
     
        public void FreeResources()
        {
            CardsModern = null;
        }
        public CardModernModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;

                if (_selectedItem == null)
                    return;

                SelectedItem = null;
            }
        }


    }
}

using System;
using System.Collections.Specialized;
using System.Reflection;

namespace BaseUtils
{
    public enum NomenclatorType : int
    {
        [EnumDescription("Nedefinit")]
        [PrimaryKeyField("ID")]
        Undefined = 0,

        #region WindRamps
        //se va pastra intervalul de nomenclatortype-uri de la -1 la -100 pentru aplicatia WindRamps pentru a preveni suprapunerea valorilor
        [EnumDescription("Nomenclator rampe")]
        [PrimaryKeyField("ID")]
        WR_Nomenclator_Rampe = -1,
        [EnumDescription("Nomenclator ture")]
        [PrimaryKeyField("ID")]
        WR_Nomenclator_Ture = -2,
        #endregion WindRamps

        [EnumDescription("Lista valute")]
        [PrimaryKeyField("Valuta_ID")]
        Lista_Valute = 1,
        [EnumDescription("Nomenclator unitati de masura")]
        [PrimaryKeyField("ID")]
        Nomenclator_UnitatiMasura = 2,
        [EnumDescription("Nomenclator subcentre de profit")]
        [PrimaryKeyField("ID")]
        Nomenclator_SubcentreDeProfit = 3,
        [EnumDescription("Nomenclator centre de profit")]
        [PrimaryKeyField("ID")]
        Nomenclator_Centre_Profit = 4,
        [EnumDescription("Lista conturi")]
        [PrimaryKeyField("Cont_ID")]
        Lista_Conturi = 7,
        [EnumDescription("Nomenclator centre de cost")]
        [PrimaryKeyField("ID")]
        Nomenclator_CentreDeCost = 9,
        [EnumDescription("Nomenclator conturi")]
        [PrimaryKeyField("ID")]
        Nomenclator_Conturi = 10,
        [EnumDescription("Lista banci")]
        [PrimaryKeyField("Banca_ID")]
        Lista_Banci = 11,
        [EnumDescription("Categorii de firme")]
        [PrimaryKeyField("CategoriiFirme_ID")]
        Lista_Categorii_Firme = 12,
        [EnumDescription("Lista functii")]
        [PrimaryKeyField("Functie_ID")]
        Lista_Functii = 13,
        [EnumDescription("Lista tari")]
        [PrimaryKeyField("Tara_ID")]
        Lista_Tari = 14,
        [EnumDescription("Lista orase")]
        [PrimaryKeyField("Oras_ID")]
        Lista_Orase = 15,
        [EnumDescription("Lista judete")]
        [PrimaryKeyField("Judet_ID")]
        Lista_Judete = 16,
        [EnumDescription("Clase autovehicule")]
        [PrimaryKeyField("FelCT_ID")]
        Lista_Feluri_CT = 17,
        [EnumDescription("Tipuri de cap tractor")]
        [PrimaryKeyField("TipCT_ID")]
        Lista_Tipuri_CT = 18,
        [EnumDescription("Marci masini")]
        [PrimaryKeyField("Marca_Masina_ID")]
        Lista_Marci_Masini = 19,
        [EnumDescription("Tipuri de marfa")]
        [PrimaryKeyField("NaturaMarfa_ID")]
        Lista_Natura_Marfa = 20,
        [EnumDescription("Tipuri de card")]
        [PrimaryKeyField("TipDoc_ID")]
        Lista_Tip_Card = 21,
        [EnumDescription("Lista documente fiscale")]
        [PrimaryKeyField("DocumentFiscal_ID")]
        Lista_Documente_Fiscale = 22,
        [EnumDescription("Tipuri de asigurari")]
        [PrimaryKeyField("TipAsigurare_ID")]
        Lista_Tipuri_Asigurari = 23,
        [EnumDescription("Lista comment")]
        [PrimaryKeyField("ID")]
        Lista_Comment = 24, // nu mai exista
        [EnumDescription("Tipuri de finantari")]
        [PrimaryKeyField("ID")]
        Nomenclator_Lista_Tipuri_Finantari = 25,
        [EnumDescription("Tipuri de autorizatii")]
        [PrimaryKeyField("TipAutorizatie_ID")]
        Lista_Tipuri_Autorizatii = 26,
        [EnumDescription("Lista cote TVA")]
        [PrimaryKeyField("CotaTVA_ID")]
        Lista_Cota_TVA = 27,
        [EnumDescription("Clase cheltuieli pentru decont cursa")]
        [PrimaryKeyField("DCCheltuiala_ID")]
        Lista_CC_Decont_Cursa = 28,
        [EnumDescription("Clase cheltuieli pentru alte cheltuieli")]
        [PrimaryKeyField("ChelClasa_ID")]
        Lista_CC_Altele_Din_Avansuri = 29, // nu mai exista
        //[EnumDescription("Clase cheltuieli pentru consum")]
        //[PrimaryKeyField("ClasaConsum_ID")]
        //Lista_CC_Consum = 30,
        [EnumDescription("Clase cheltuieli pentru management")]
        [PrimaryKeyField("CCManagement_ID")]
        Lista_CC_Management = 31,
        [EnumDescription("Clase cheltuieli pentru parc auto")]
        [PrimaryKeyField("CPAuto_ID")]
        Lista_CC_PAuto = 32, // nu mai exista
        [EnumDescription("Subclase cheltuieli pentru parc auto")]
        [PrimaryKeyField("CPAutoSubclasa_ID")]
        Lista_CC_Subclasa_PAuto = 33, // nu mai exista
        [EnumDescription("Lista categorie clase cheltuieli")]
        [PrimaryKeyField("ClasaCategorie_ID")]
        Lista_Clasa_Categorie = 34,
        //[EnumDescription("Lista documente gestiune")]
        //[PrimaryKeyField("ID")]
        //Lista_Documente_Gestiune = 35,
        [EnumDescription("Lista denumiri operatii")]
        [PrimaryKeyField("DenumireOperatie_ID")]
        Denumire_Operatie = 37,
        [EnumDescription("Lista denumiri reparatii")]
        [PrimaryKeyField("Tip_Reparatie_ID")]
        Lista_Repartaii = 38,
        //[EnumDescription("Lista obiecte de inventar mecanici")]
        //[PrimaryKeyField("ObiectInventar_ID")]
        //Lista_Obiecte_De_Inventar_Mecanic = 39,
        //[EnumDescription("Lista obiecte de inventar masina")]
        //[PrimaryKeyField("ObiectInventar_ID")]
        //Lista_Obiecte_De_Inventar_Masina_Sofer = 40,
        [EnumDescription("Lista gestiuni")]
        [PrimaryKeyField("Gestiune_ID")]
        Lista_Gestiuni = 41,
        [EnumDescription("Tipuri documente justificative banca")]
        [PrimaryKeyField("ID")]
        Nomenclator_DocBanca_TipDocJustificativ = 42,
        [EnumDescription("Tipuri documente plata banca")]
        [PrimaryKeyField("ID")]
        Nomenclator_DocBanca_TipDocPlata = 43,
        [EnumDescription("Tipuri documente justificative casa")]
        [PrimaryKeyField("ID")]
        Nomenclator_DocCasa_TipDocJustificativ = 44,
        [EnumDescription("Tipuri documente plata casa")]
        [PrimaryKeyField("ID")]
        Nomenclator_DocCasa_TipDocPlata = 45,
        [EnumDescription("Tipuri regimuri de transport")]
        [PrimaryKeyField("ID")]
        Nomenclator_RegimuriTransport = 46,
        [EnumDescription("Lista scop dispozitie")]
        [PrimaryKeyField("ID")]
        Nomenclator_ScopDispozitie = 47,
        [EnumDescription("Titulatura")]
        [PrimaryKeyField("ID")]
        Nomenclator_Sex = 48,
        [EnumDescription("Venituri cheltuieli")]
        [PrimaryKeyField("ID")]
        Nomenclator_VenitCheltuieli = 49,
        [EnumDescription("Lista grupe")]
        [PrimaryKeyField("Grupa_ID")]
        MF_Lista_Grupe = 50,
        [EnumDescription("Lista subgrupe")]
        [PrimaryKeyField("Subgrupa_ID")]
        MF_Lista_Subgrupe = 51,
        //[EnumDescription("Lista servicii descarcare de gestiune")]
        //[PrimaryKeyField("Articol_ID")]
        //Lista_Servicii_Facturi_Descarcare_Gestiune = 54,
        [EnumDescription("Lista categorie registru casa")]
        [PrimaryKeyField("CategorieCasa_ID")]
        Lista_Categorie_Casa = 55,
        [EnumDescription("Lista categorie registru banca")]
        [PrimaryKeyField("CategorieBanca_ID")]
        Lista_Categorie_Banca = 56,
        [EnumDescription("Lista conturi profit si pierdere")]
        [PrimaryKeyField("ID_Modul")]
        Conturi_Profit_Pierdere = 57,
        [EnumDescription("Tipuri expediere registratura")]
        [PrimaryKeyField("ID")]
        Nomenclator_Registratura_TipExpediere = 58,
        [EnumDescription("Tipuri stari MF")]
        [PrimaryKeyField("ID")]
        Nomenclator_Stari_MF = 59,
        [EnumDescription("Lista case")]
        [PrimaryKeyField("FirmePropriiCasa_ID")]
        Firme_Proprii_Casa = 60,
        [EnumDescription("Grupe de produse")]
        [PrimaryKeyField("Grupa_ID")]
        Nomenclator_Grupe_Produse = 61,
        [EnumDescription("Lista operatiuni")]
        [PrimaryKeyField("ID")]
        Lista_Operatiuni = 62,
        [EnumDescription("Tipuri de transport")]
        [PrimaryKeyField("ID")]
        Lista_Fel_Transport = 63,
        [EnumDescription("Lista incadrari")]
        [PrimaryKeyField("IncadrareDetalii_ID")]
        Lista_Incadrari = 64,
        [EnumDescription("Tipuri operatiuni magazie")]
        [PrimaryKeyField("ID")]
        Nomenclator_TipOperatiuniMagazie = 65,
        [EnumDescription("Tipuri operatiuni jurnale")]
        [PrimaryKeyField("DenumireOperatiune_ID")]
        Lista_Operatiuni_Jurnale = 66,
        [EnumDescription("Serii facturi")]
        [PrimaryKeyField("SerieFactura_ID")]
        SerieFacturi = 67,
        [EnumDescription("Tipuri impachetare marfa")]
        [PrimaryKeyField("ID")]
        Nomenclator_ImpachetareMarfa = 68,
        [EnumDescription("Tipuri documente insotire")]
        [PrimaryKeyField("ID")]
        Lista_Documente_Insotire = 69,
        [EnumDescription("Lista facilitati")]
        [PrimaryKeyField("ID")]
        Lista_Facilitati = 70,
        [EnumDescription("Feluri colete")]
        [PrimaryKeyField("ID")]
        Lista_Fel_Colete = 71,
        [EnumDescription("Servicii facturi engleza")]
        [PrimaryKeyField("ID")]
        Nomenclator_Servicii_Facturi_Eng = 72,
        [EnumDescription("Tipuri documente registratura")]
        [PrimaryKeyField("ID")]
        Nomenclator_TipDoc_Registratura = 73,
        [EnumDescription("Lista servicii facturi")]
        [PrimaryKeyField("Articol_ID")]
        Lista_Servicii_Facturi = 74,
        [EnumDescription("Lista produse")]
        [PrimaryKeyField("Produs_ID")]
        Lista_Produse = 75,
        [EnumDescription("Tipuri repartizare costuri")]
        [PrimaryKeyField("ID")]
        Nomenclator_TipRepartizareCosturi = 76,
        [EnumDescription("Tipuri repartizare TVA")]
        [PrimaryKeyField("ID")]
        Nomenclator_TipRepartizareTVA = 77,
        [EnumDescription("Nomenclator clase cash flow")]
        [PrimaryKeyField("ID")]
        Nomenclator_CC_Management = 78,
        [EnumDescription("Nomenclator documente cursa")]
        [PrimaryKeyField("ID")]
        Nomenclator_Documente_Cursa = 79,
        [EnumDescription("Lista conturi banca")]
        [PrimaryKeyField("FirmePropriiConturi_ID")]
        Firme_Proprii_Conturi = 80,
        [EnumDescription("Tipuri tarifare")]
        [PrimaryKeyField("ID")]
        Nomenclator_TipTarifarePartida = 81,
        [EnumDescription("Clase cheltuieli")]
        [PrimaryKeyField("Clasa_ID")]
        Nomenclator_ClasaCheltuieli = 82,
        [EnumDescription("Lista clase de poluare masini")]
        [PrimaryKeyField("ID")]
        Nomenclator_Clase_De_Poluare_Masini = 83,
        [EnumDescription("Tipuri licente masini")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tipuri_Licente_Masini = 84,
        [EnumDescription("Lista categorii masini")]
        [PrimaryKeyField("ID")]
        Nomenclator_Categorii_Masini = 85,
        [EnumDescription("Lista scadente")]
        [PrimaryKeyField("ID")]
        Nomenclator_Date_Scadente = 86,
        [EnumDescription("Nomenclator anotimpuri consumuri")]
        [PrimaryKeyField("ID")]
        Nomenclator_Anotimpuri_Consum = 87,
        [EnumDescription("Nomenclator carduri de alimentare masini")]
        [PrimaryKeyField("ID")]
        Nomenclator_CarduriMasini = 88,
        [EnumDescription("Nomenclator cosumabile schimburi auto")]
        [PrimaryKeyField("ID")]
        Nomenclator_Consumabile_Schimburi_Auto = 89,
        [EnumDescription("Nomenclator marci anvelope")]
        [PrimaryKeyField("ID")]
        Nomenclator_Marci_Anvelope = 90,
        [EnumDescription("Nomenclator profile anvelope")]
        [PrimaryKeyField("ID")]
        Nomenclator_Profile_Anvelope = 91,
        [EnumDescription("Nomenclator tipuri daune")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tipuri_Daune = 92,
        [EnumDescription("Nomenclator tipuri documente daune")]
        [PrimaryKeyField("ID")]
        Nomenclator_TipDoc_Daune = 93,
        [EnumDescription("Nomenclator coeficienti de drum")]
        [PrimaryKeyField("ID")]
        Nomenclator_Coeficienti_De_Drum = 94,
        [EnumDescription("Tipuri marfa")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Natura_Marfa = 95,
        [EnumDescription("Capabilitati marfa")]
        [PrimaryKeyField("ID")]
        Nomenclator_Capabilitati_Tip_Marfa = 96,
        [EnumDescription("Nomenclator obiecte de inventar")]
        [PrimaryKeyField("ID")]
        Nomenclator_Obiecte_De_Inventar = 97,
        [EnumDescription("Nomenclator tipuri de zbor")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Zbor = 98,
        [EnumDescription("Nomenclator categorii de zbor")]
        [PrimaryKeyField("ID")]
        Nomenclator_Categorie_Zbor = 99,
        [EnumDescription("Nomenclator status reparatie")]
        [PrimaryKeyField("ID")]
        Nomenclator_Status_Reparatie = 100,
        [EnumDescription("Tipuri operatii daune")]
        [PrimaryKeyField("ID")]
        Nomenclator_TipOperatie_Reparatie = 101,
        [EnumDescription("Nomenclator card furnizori")]
        [PrimaryKeyField("ID")]
        Nomenclator_Card_Furnizori = 102,
        [EnumDescription("Nomenclator card produse")]
        [PrimaryKeyField("ID")]
        Nomenclator_Card_Produse = 103,
        [EnumDescription("Nomenclator locatii magazie")]
        [PrimaryKeyField("ID")]
        Nomenclator_ListaLocatieMagazie = 104,
        [EnumDescription("Nomenclator grupuri de firme")]
        [PrimaryKeyField("GrupaFirma_ID")]
        Firme_Grupuri = 105,
        [EnumDescription("Nomenclator tipuri mijloace fixe")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_MF = 106,
        [EnumDescription("Nomenclator tipuri expediere")]
        [PrimaryKeyField("ID")]
        Nomenclator_TipExpediere_Registratura = 107,
        [EnumDescription("Nomenclator tip cazier angajati")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Cazier_Angajati = 108,
        [EnumDescription("Nomenclator matrice tarife pe tari")]
        [PrimaryKeyField("ID")]
        Nomenclator_Matrici_Tarife_Tari = 109,
        [EnumDescription("Nomenclator conversii unitati de masura")]
        [PrimaryKeyField("ID")]
        Nomenclator_UM_Conversii = 110,
        [EnumDescription("Nomenclator tarife de grupaj")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tarife_Grupaj = 111,
        [EnumDescription("Nomenclator culori date scadente")]
        [PrimaryKeyField("ScadentaColor_ID")]
        Nomenclator_Scadente_Culori = 112,
        [EnumDescription("Nomenclator rute negociate")]
        [PrimaryKeyField("ID")]
        Nomenclator_Rute_Negociate = 113,
        [EnumDescription("Nomenclator subgrupe produse")]
        [PrimaryKeyField("ID")]
        Nomenclator_Subgrupe_Produse = 114,
        [EnumDescription("Nomenclator marci produse")]
        [PrimaryKeyField("ID")]
        Nomenclator_Marci_Produse = 115,
        [EnumDescription("Nomenclator tipuri produse")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tipuri_Produse = 116,
        [EnumDescription("Nomenclator producatori")]
        [PrimaryKeyField("ID")]
        Nomenclator_Producatori = 117,
        [EnumDescription("Nomenclator coduri modele")]
        [PrimaryKeyField("ID")]
        Nomenclator_Coduri_Modele = 118,
        [EnumDescription("Nomenclator mod preluare comanda service")]
        [PrimaryKeyField("ID")]
        Nomenclator_Mod_Preluare_Comanda_Service = 119,
        [EnumDescription("Nomenclator echivalente produse")]
        [PrimaryKeyField("ID")]
        Nomenclator_Echivalente_Produse = 120,
        [EnumDescription("Nomenclator tip comanda service")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Comanda_Service = 121,
        [EnumDescription("Nomenclator grupe obiecte de inventar")]
        [PrimaryKeyField("ID")]
        Nomenclator_Grupe_Obiecte_De_Inventar = 122,
        [EnumDescription("Nomenclator perioade date scadente")]
        [PrimaryKeyField("PeriodColor_ID")]
        Nomenclator_Perioade_Culori = 123,
        [EnumDescription("Nomenclator sectii service")]
        [PrimaryKeyField("ID")]
        Nomenclator_Sectii_Service = 124,
        [EnumDescription("Nomenclator tip masini operatii service")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Masini_Operatii_Service = 125,
        [EnumDescription("Nomenclator rampe service")]
        [PrimaryKeyField("ID")]
        Nomenclator_Rampe_Service = 126,
        [EnumDescription("Nomenclator tip lucrari service")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Lucrari_Service = 127,
        [EnumDescription("Nomenclator documente gestiune")]
        [PrimaryKeyField("ID")]
        Nomenclator_Documente_Gestiune = 128,
        [EnumDescription("Nomenclator tip cursa")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Cursa = 129,
        [EnumDescription("Tip de transport TVA")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Transport = 130,
        [EnumDescription("Matrice transport TVA")]
        [PrimaryKeyField("ID")]
        Nomenclator_Matrice_Transporturi = 131,
        [EnumDescription("Calatori Grupe Traseu")]
        [PrimaryKeyField("ID")]
        Calatori_Lista_Grupe_Trasee = 132,
        [EnumDescription("Calatori Grupe Traseu")]
        [PrimaryKeyField("ID")]
        Calatori_Lista_Trasee = 133,
        [EnumDescription("Calatori operatiuni Traseu")]
        [PrimaryKeyField("ID")]
        Calatori_Lista_Operatiuni_Trasee = 134,
        [EnumDescription("Nomenclator nr auto - tipuri de marfa")]
        [PrimaryKeyField("ID")]
        Nomenclator_NrAuto_Marfa = 135,
        //[EnumDescription("Nomenclator tip lucrari comandate service")]
        //[PrimaryKeyField("ID")]
        //Nomenclator_Tip_Lucrare_Comanda_Service = 136,
        [EnumDescription("Instante de judecata")]
        [PrimaryKeyField("ID")]
        Nomenclator_Juridic_Instante_De_Judecata = 137,
        [EnumDescription("Natura litigiilor")]
        [PrimaryKeyField("ID")]
        Nomenclator_Juridic_Natura_Litigii = 138,
        [EnumDescription("Faza procesuale")]
        [PrimaryKeyField("ID")]
        Nomenclator_Juridic_Faze_Procesuale = 139,
        [EnumDescription("Matrice TVA")]
        [PrimaryKeyField("ID")]
        Nomenclator_Matrice_TVA = 140,
        [EnumDescription("Tip Tara")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Tari = 141,
        [EnumDescription("Loc montare")]
        Nomenclator_Locuri_Montare_Anvelope = 142,
        [EnumDescription("Tip dispozitie")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Dispozitie = 143,
        [EnumDescription("Tip camion caraus")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Camioane_Carausi = 144,
        [EnumDescription("Clase ADR")]
        [PrimaryKeyField("ID")]
        Nomenclator_Clase_ADR = 145,
        [EnumDescription("Aeroporturi")]
        [PrimaryKeyField("ID")]
        Nomenclator_Aeroporturi = 146,
        [EnumDescription("Porturi")]
        [PrimaryKeyField("ID")]
        Nomenclator_Porturi = 147,
        [EnumDescription("Tip nonconformitate")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Nonconformitate = 148,
        [EnumDescription("Tip document vamal")]
        [PrimaryKeyField("ID")]
        Nomenclator_Documente_Vamale = 149,
        [EnumDescription("Tip facilitate vama")]
        [PrimaryKeyField("ID")]
        Nomenclator_Facilitati_Vama = 150,
        [EnumDescription("Tip operatiune vama")]
        [PrimaryKeyField("ID")]
        Nomenclator_Operatiuni_Vama = 151,
        [EnumDescription("Tip solutie")]
        [PrimaryKeyField("ID")]
        Nomenclator_Juridic_Investire = 152,
        [EnumDescription("Executori")]
        [PrimaryKeyField("Executor_ID")]
        Lista_Juridic_Executori = 153,
        [EnumDescription("Tip soferi pentru diurna")]
        [PrimaryKeyField("CalatoriListaTipSofer_ID")]
        Calatori_Lista_Tip_Sofer = 154,
        [EnumDescription("Configurare diurna")]
        [PrimaryKeyField("ID")]
        Calatori_Diurna = 155,
        [EnumDescription("Zone expeditie")]
        [PrimaryKeyField("ID")]
        Nomenclator_Zone_Expeditie = 156,
        [EnumDescription("Tip tarife expeditie")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Tarife_Expeditie = 157,
        [EnumDescription("Tip lot marfa subcentre cost")]
        [PrimaryKeyField("ID")]
        Nomenclator_Lot_Marfa_Subcentre_Cost = 296,
        [EnumDescription("Tarife - terminal - terminal si shuttle")]
        [PrimaryKeyField("Tarif_ID")]
        Lot_Marfa_Tarif_Terminal_Terminal = 297,
        [EnumDescription("Lista operatii GE")]
        [PrimaryKeyField("ID")]
        Nomenclator_Operatii_GE_Importate = 298,
        [EnumDescription("Nomenclator grupe de adaos pe produse")]
        [PrimaryKeyField("ID")]
        Nomenclator_Grupe_Adaos_Produse = 299,
        [EnumDescription("Nomenclator tancuri combustibil")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tancuri_Combustibil = 300,
        [EnumDescription("Nomenclator rute comenzi ferry")]
        [PrimaryKeyField("ID")]
        Nomenclator_Rute_Comenzi_Ferry = 301,
        [EnumDescription("Nomenclator auto comenzi ferry")]
        [PrimaryKeyField("ID")]
        Nomenclator_Auto_Comenzi_Ferry = 302,
        [EnumDescription("Nomenclator tipuri clienti furnizori cash flow")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tipuri_Clienti_Furnizori = 303,
        [EnumDescription("Nomenclator lista operatiuni pe traseu")]
        [PrimaryKeyField("ID")]
        Calatori_Lista_Operatiuni_Programator = 304,
        [EnumDescription("Nomenclator dimensiuni masini Ferry")]
        [PrimaryKeyField("ID")]
        Nomenclator_Dimensiuni_Ferry = 305,
        [EnumDescription("Nomenclator linii aeriene")]
        [PrimaryKeyField("ID")]
        Nomenclator_Linii_Aeriene = 306,
        [EnumDescription("Aerian / Maritim - lista servicii")]
        [PrimaryKeyField("Articol_ID")]
        Lista_Servicii_Aerian_Maritim = 307,
        [EnumDescription("Aerian & Maritim - lista servicii linii")]
        [PrimaryKeyField("ID")]
        Lista_Servicii_Linii_Aerian_Maritim = 308,
        [EnumDescription("Comanda Ferry - preturi rute speciale")]
        [PrimaryKeyField("ID")]
        Lista_Preturi_Rute_Speciale_Ferry = 309,
        [EnumDescription("Nomenclator concedii")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Concedii = 310,
        [EnumDescription("Nomenclator departamente curse")]
        [PrimaryKeyField("ID")]
        Nomenclator_Departamente_Curse = 311,
        [EnumDescription("Filiale firma proprie")]
        [PrimaryKeyField("Filiala_ID")]
        Filiale = 312,
        [EnumDescription("Manager filiale firma proprie")]
        [PrimaryKeyField("ManagerFiliala_ID")]
        Manager_Filiale = 313,
        [EnumDescription("Nomenclator terminale")]
        [PrimaryKeyField("ID")]
        Nomenclator_Terminale = 314,
        [EnumDescription("Nomenclator tip container")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tipuri_Container = 315,
        [EnumDescription("Lista linii maritime")]
        [PrimaryKeyField("ID")]
        Nomenclator_Linii_Maritime = 316,
        [EnumDescription("Dimensiuni vehicul")]
        [PrimaryKeyField("ID")]
        Nomenclator_DimensiuniVehicul = 317,
        [EnumDescription("Descriere anexe")]
        [PrimaryKeyField("ID")]
        Nomenclator_Anexa_Comanda = 318,
        [EnumDescription("Stadii dosar")]
        [PrimaryKeyField("ID")]
        Nomenclator_Stadii_Dosar = 319,
        [EnumDescription("Nomenclator tip revizii")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Revizii = 320,
        [EnumDescription("Nomenclator tip operatiuni service")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Operatiuni_Service = 321,
        [EnumDescription("Durata contract munca")]
        [PrimaryKeyField("ID")]
        Nomenclator_Durata_Contract = 322,
        [EnumDescription("Tip norma la contract de munca")]
        [PrimaryKeyField("ID")]
        Nomenclator_Norme_CIM = 323,
        [EnumDescription("Nomenclator tip tarifare operatii service")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Tarifare_Operatii_Service = 324,
        [EnumDescription("Nomenclator ore lucrate pe rute")]
        [PrimaryKeyField("ID")]
        Calatori_Lista_Rute_International = 325,
        [EnumDescription("Nomenclator statusuri depozit")]
        [PrimaryKeyField("ID")]
        Nomenclator_Statusuri_Depozit = 326,
        [EnumDescription("Nomenclator categorii note adiacente")]
        [PrimaryKeyField("ID")]
        Nomenclator_Categorii_Note_Adiacente = 327,
        [EnumDescription("Lista conturi nedeductibile")]
        [PrimaryKeyField("ID")]
        Lista_Conturi_Nedeductibile = 328,
        [EnumDescription("Nomenclator tip cheltuiala deductibila")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Cheltuiala_Deductibila = 329,
        [EnumDescription("Lista tarife aerian")]
        [PrimaryKeyField("Tarif_ID")]
        Lista_Tarife_Aerian = 330,
        [EnumDescription("Nomenclator tipuri concedii si absente")]
        [PrimaryKeyField("ID")]
        Nomenclator_TipAbsenta = 331,
        [EnumDescription("Nomenclator CRM tip de transport")]
        [PrimaryKeyField("ID")]
        Nomenclator_CRM_TransportType = 332,
        [EnumDescription("Nomenclator CRM tip de incarcari")]
        [PrimaryKeyField("ID")]
        Nomenclator_CRM_IncarcariType = 333,
        [EnumDescription("Nomenclator CRM conditi de livrare")]
        [PrimaryKeyField("ID")]
        Nomenclator_CRM_CondLiv = 334,
        [EnumDescription("Note contabile salarii")]
        [PrimaryKeyField("Note_Contabile_Salarii_ID")]
        Note_Contabile_Salarii = 335,
        [EnumDescription("Nomenclator Oferte Directie")]
        [PrimaryKeyField("ID")]
        Nomenclator_Oferte_Directie = 336,
        [EnumDescription("Nomenclator Oferte Tip Serviciu")]
        [PrimaryKeyField("ID")]
        Nomenclator_Oferte_Tip_Serviciu = 337,
        [EnumDescription("Nomenclator Destinatii")]
        [PrimaryKeyField("Destinatie_ID")]
        Calatori_Nomenclator_Destinatie = 338,
        [EnumDescription("Nomenclator Tronsoane")]
        [PrimaryKeyField("ID")]
        Calatori_Nomenclator_Tronsoane = 339,
        [EnumDescription("Lista tip auto anvelope")]
        [PrimaryKeyField("ListaTipAutoAnvelope_ID")]
        ListaTipAutoAnvelope = 340,
        [EnumDescription("Tip operatiuni anvelope")]
        [PrimaryKeyField("TipOperatiuniAnvelope_ID")]
        TipOperatiuniAnvelope = 341,
        [EnumDescription("Bilant Nomenclator Tipuri Formular")]
        [PrimaryKeyField("Formular_ID")]
        Bilant_Nomenclator_Tipuri_Formular = 342,
        [EnumDescription("Bilant Nomenclator Clasificari")]
        [PrimaryKeyField("Clasificare_ID")]
        Bilant_Nomenclator_Clasificari = 343,
        [EnumDescription("Bilant Formule Calcul")]
        [PrimaryKeyField("Calcul_ID")]
        Bilant_Formule_Calcul = 344,
        [EnumDescription("Nomenclator_Sursa_Energie")]
        [PrimaryKeyField("SursaEnergie_ID")]
        Nomenclator_Sursa_Energie = 345,
        [EnumDescription("Nomenclator Tip Consum TK")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Consum_TK = 346,
        [EnumDescription("Nomenclator Diurna Sofer")]
        [PrimaryKeyField("ID")]
        Nomenclator_Diurna_Sofer = 347,
        [EnumDescription("Tip proces verbal mijloace fixe/obiecte de inventar")]
        [PrimaryKeyField("ID")]
        MF_Tip_ProcesVerbal = 348,
        [EnumDescription("Status soferi")]
        [PrimaryKeyField("Status_ID")]
        Nomenclator_Status_Soferi_Curse = 349,
        [EnumDescription("Nomenclator tip consumuri")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Consum = 350,
        [EnumDescription("Nomenclator tip Gps")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_GPS = 351,
        [EnumDescription("Nomenclator tip export")]
        [PrimaryKeyField("TipExport_ID")]
        Nomenclator_TipExportDate = 352,
        [EnumDescription("Nomenclator tip raport EBIDTA")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Raport_EBIDTA = 353,
        [EnumDescription("Nomenclator indicatori EBIDTA")]
        [PrimaryKeyField("ID")]
        Nomenclator_Indicatori_EBIDTA = 354,
        [EnumDescription("Nomenclator formule de calcul indicatori EBIDTA")]
        [PrimaryKeyField("ID")]
        Nomenclator_Indicatori_EBIDTA_Formule_Calcul = 355,
        [EnumDescription("Nomenclator tip agregate")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tipuri_Agregat = 356,
        [EnumDescription("Nomenclator parametrii")]
        [PrimaryKeyField("Parametru_ID")]
        Parametri = 357,
        [EnumDescription("Nomenclator_Rapoarte")]
        [PrimaryKeyField("ID")]
        Nomenclator_Rapoarte = 358,
        [EnumDescription("Nomenclator_Consum_Suplimentar")]
        [PrimaryKeyField("ID")]
        Nomenclator_Consum_Suplimentar = 359,
        [EnumDescription("Nomenclator_Tip_Documente")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Documente = 360,
        [EnumDescription("Nomenclator_Departamente")]
        [PrimaryKeyField("ID")]
        Nomenclator_Departamente = 361,

        #region TransporturiTrust (doar pentru Dolotrans)
        [EnumDescription("Nomenclator marci vehicule Trust")]
        [PrimaryKeyField("ID")]
        Nomenclator_Marci_Vehicule = 362,
        [EnumDescription("Nomenclator tipuri caroserie Trust")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tipuri_Caroserie = 363,
        [EnumDescription("Nomenclator tarife transport vehicule Trust")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tarife_Transport_Vehicule = 364,
        #endregion TransporturiTrust (doar pentru Dolotrans)

        [EnumDescription("Nomenclator cauze intarziere")]
        [PrimaryKeyField("CauzaIntarziere_ID")]
        Nomenclator_CauzaIntarziere = 365,
        [EnumDescription("Nomenclator flote")]
        [PrimaryKeyField("ID")]
        Nomenclator_Flote = 366,
        [EnumDescription("Nomenclator categorie tonaj")]
        [PrimaryKeyField("ID")]
        Nomenclator_Categorie_Tonaj = 367,
        [EnumDescription("Nomenclator motive indisponibilitate masini")]
        [PrimaryKeyField("ID")]
        Nomenclator_Motiv_Indisponibilitate = 368,
        [EnumDescription("Nomenclator diurna pe tara")]
        [PrimaryKeyField("ID")]
        Nomenclator_DiurnaXTara = 369,
        [EnumDescription("Nomenclator diurna extern")]
        [PrimaryKeyField("ID")]
        Nomenclator_Diurna_Extern = 370,
        [EnumDescription("Serii avize")]
        [PrimaryKeyField("SerieAviz_ID")]
        SerieAvize = 371,
        [EnumDescription("Serii chitante")]
        [PrimaryKeyField("SerieChitanta_ID")]
        SerieChitante = 372,
        [EnumDescription("Nomenclator tipuri rapoarte")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tipuri_Rapoarte = 373,
        [EnumDescription("Nomenclator_Subdepartamente")]
        [PrimaryKeyField("ID")]
        Nomenclator_Subdepartamente = 374,


        [EnumDescription("Lista cod taxare inversa")]
        [PrimaryKeyField("CodTaxareInversa_ID")]
        Lista_Cod_TaxareInversa = 5555,

        [EnumDescription("Lista cod CAEN")]
        [PrimaryKeyField("CodCAEN_ID")]
        Lista_Cod_CAEN = 5556,

        [EnumDescription("Nomenclator Cauze")]
        [PrimaryKeyField("ID")]
        Nomenclator_Cauze = 375,

        [EnumDescription("Nomenclator clasificari planificare")]
        [PrimaryKeyField("ID")]
        Nomenclator_Clasificari_Planificare = 376,
        [EnumDescription("Nomenclator Coloane Planificator")]
        [PrimaryKeyField("ID")]
        Nomenclator_Coloane_Planificator = 377,
        [EnumDescription("Nomenclator Tipuri Culori Planificare 1")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tipuri_Culori_Planificare_1 = 378,
        [EnumDescription("Nomenclator Tipuri Culori Planificare 2")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tipuri_Culori_Planificare_2 = 379,
        [EnumDescription("Nomenclator Clasificari Asignare Coloane Planificator")]
        [PrimaryKeyField("ID")]
        Nomenclator_Clasificari_Asignare_Coloane_Planificator = 380,
        [EnumDescription("Nomenclator statusuri cursa")]
        [PrimaryKeyField("ID")]
        Nomenclator_Statusuri_Cursa = 381,
        [EnumDescription("Nomenclator proiecte cursa")]
        [PrimaryKeyField("ID")]
        Nomenclator_Curse_Proiect = 382,
        [EnumDescription("Nomenclator filtre predefinite")]
        [PrimaryKeyField("ID")]
        Nomenclator_Filtre_Predefinite = 383,
        [EnumDescription("Nomenclator initiator poprire")]
        [PrimaryKeyField("ID")]
        Nomenclator_Initiator_Poprire = 384,
        [EnumDescription("Coduri statii Shell")]
        [PrimaryKeyField("ID")]
        Coduri_Statii_Shell = 385,
        [EnumDescription("Grupe gestiuni transfer")]
        [PrimaryKeyField("ID")]
        Grupe_Gestiuni_Transfer = 386,
        [EnumDescription("Nomenclator tip rate")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Rate_Asigurari = 387,

        [EnumDescription("Nomenclator tip marfa depozit")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Marfa_Depozit = 388,
        [EnumDescription("Nomenclator produse depozit")]
        [PrimaryKeyField("ID")]
        Nomenclator_Produse_Depozit = 389,
        [EnumDescription("Nomenclator depozite")]
        [PrimaryKeyField("ID")]
        Nomenclator_Depozite = 390,
        [EnumDescription("Nomenclator tarife depozit")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tarife_Depozit = 391,
        [EnumDescription("Nomenclator km tarifare")]
        [PrimaryKeyField("ID")]
        Nomenclator_KM_Tarifare = 392,
        [EnumDescription("Nomenclator tarife")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tarife = 393,
        [EnumDescription("Nomenclator tip contracte")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Contracte = 394,
        [EnumDescription("Nomenclator centre profit indicatori EBIDTA")]
        [PrimaryKeyField("ID")]
        Nomenclator_Indicatori_EBIDTA_Centre_Profit = 395,

        [EnumDescription("Nomenclator Tip Cutie Viteze")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Cutie_Viteze = 396,
        [EnumDescription("Nomenclator coduri cisterna")]
        [PrimaryKeyField("ID")]
        Nomenclator_Coduri_Cisterna = 398,
        [EnumDescription("Nomenclator corp masurator cisterna")]
        [PrimaryKeyField("ID")]
        Nomenclator_Corp_Masurator_Cisterna = 399,
        [EnumDescription("Nomenclator tip calculator cisterna")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Calculator_Cisterna = 400,
        [EnumDescription("Nomenclator produse transportate")]
        [PrimaryKeyField("ID")]
        Nomenclator_Produse_Transportate = 401,
        [EnumDescription("Modalitate de plata")]
        [PrimaryKeyField("ID")]
        Nomenclator_TipDocPlata = 402,

        [EnumDescription("Lista servicii bon fiscal")]
        [PrimaryKeyField("ID")]
        Lista_Servicii_Bon_Fiscal = 403,

        [EnumDescription("Nomenclator tarife transport")]
        [PrimaryKeyField("Tarif_ID")]
        Nomenclator_Tarife_Transport = 404,

        [EnumDescription("Nomenclator tipuri rapoarte popup")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tipuri_RapoartePopUp = 405,

        [EnumDescription("Nomenclator planificator status curse")]
        [PrimaryKeyField("ID")]
        Nomenclator_Planificator_Status_Curse = 406,

        [EnumDescription("Nomenclator planificator status partide")]
        [PrimaryKeyField("ID")]
        Nomenclator_Planificator_Status_Partide = 407,

        [EnumDescription("Nomenclator motive refuz")]
        [PrimaryKeyField("ID")]
        Nomenclator_Motive_Refuz = 408,

        [EnumDescription("Panouri Scadente Users")]
        [PrimaryKeyField("PanouriScadenteUsers_ID")]
        Panouri_Scadente_Users = 409,

        [EnumDescription("Nomenclator Tarife Transport Operatiuni Suplimentare")]
        [PrimaryKeyField("TarifSuplimentar_ID")]
        Nomenclator_Tarife_Transport_Operatiuni_Suplimenta = 410,

        [EnumDescription("Nomenclator diurna decont terti")]
        [PrimaryKeyField("ID")]
        Nomenclator_Diurna_DecontTerti = 411,

        [EnumDescription("Administrare Multilanguage")]
        [PrimaryKeyField("AdministrareMultilanguage_ID")]
        AdministrareMultilanguage = 412,

        [EnumDescription("Tipuri repartizare venituri")]
        [PrimaryKeyField("ID")]
        Nomenclator_TipRepartizareVenituri = 413,

        [EnumDescription("Nomenclator Tip Amenzi")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Amenda = 414,

        [EnumDescription("Nomenclator abateri disciplinare")]
        [PrimaryKeyField("ID")]
        Nomenclator_abateri_disciplinare = 415,

        [EnumDescription("Nomenclator tip documente renumerotare")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tip_Documente_Renumerotare = 416,

        [EnumDescription("Configurari useri")]
        [PrimaryKeyField("Users_Configurari_ID")]
        Users_Configurari = 417,

        [EnumDescription("Nomenclator tip comanda")]
        [PrimaryKeyField("ID")]
        Nomenclator_TipComanda = 418,

        [EnumDescription("Lista facturi neincasate")]
        [PrimaryKeyField("Factura_ID")]
        Lista_Facturi_Neincasate = 419,

        [EnumDescription("Lista facturi neplatite")]
        [PrimaryKeyField("Factura_ID")]
        Lista_Facturi_Neplatite = 420,

        [EnumDescription("Lista useri")]
        [PrimaryKeyField("User_ID")]
        Lista_Useri = 421,

        [EnumDescription("Nomenclator Export Date")]
        [PrimaryKeyField("ID")]
        Nomenclator_ExportDate = 422,

        [EnumDescription("Nomenclator puncte service")]
        [PrimaryKeyField("ID")]
        Nomenclator_Puncte_Service = 423,

        [EnumDescription("Nomenclator Tipuri Apeluri")]
        [PrimaryKeyField("ID")]
        Nomenclator_Tipuri_Apeluri = 424,

        [EnumDescription("Motiv incetare contract")]
        [PrimaryKeyField("ID")]
        Nomenclator_MotivIncetareContract = 425,

        [EnumDescription("Nomenclator Furnizor Import Comenzi GPS")]
        [PrimaryKeyField("FurnizorImportComenziGPS_ID")]
        Nomenclator_Furnizor_Import_Comenzi_GPS = 426,

        [EnumDescription("Modalitate de plata spalatorii")]
        [PrimaryKeyField("ID")]
        Nomenclator_ModalitatePlataSpalatorii = 427,


        [EnumDescription("Nomenclator Export Date Main")]
        [PrimaryKeyField("ID")]
        Nomenclator_ExportDateMain = 428,

        [EnumDescription("Nomenclator Tip Export Date Main")]
        [PrimaryKeyField("ID")]
        Nomenclator_TipExportDateMain = 429,

        [EnumDescription("Nomenclator X Export Date Main")]
        [PrimaryKeyField("ID")]
        Nomenclator_X_ExportDateMain = 430,

        [EnumDescription("Nomenclator Tip Pauza")]
        [PrimaryKeyField("ID")]
        Nomenclator_TipPauza = 431,

        [EnumDescription("Nomenclator_TipulDocumentuluiComunicat")]
        [PrimaryKeyField("ID")]
        Nomenclator_TipulDocumentuluiComunicat = 432,
        [EnumDescription("Nomenclator POI Volvo")]
        [PrimaryKeyField("ID")]
        Nomenclator_VolvoPOI = 433,

        [EnumDescription("Nomenclator vama")]
        [PrimaryKeyField("ID")]
        Nomenclator_Vama = 434,

        [EnumDescription("Nomenclator comisionar vamal")]
        [PrimaryKeyField("ID")]
        Nomenclator_ComisionarVamal = 435,

        [EnumDescription("Clauze diesel perioada calcul")]
        [PrimaryKeyField("ID")]
        Nomenclator_ClauzePerioadaCalcul = 436,

        [EnumDescription("Clauze diesel mod calcul")]
        [PrimaryKeyField("ID")]
        Nomenclator_ClauzeModCalcul = 437,

        [EnumDescription("Nomenclator stare contract")]
        [PrimaryKeyField("ID")]
        Nomenclator_StareContract = 438,

        [EnumDescription("Nomenclator tip tranzactie banca")]
        [PrimaryKeyField("TipTranzactie_ID")]
        Nomenclator_TipTranzactieBanca = 439,
        [EnumDescription("Lista Tipuri CT Nou")]
        [PrimaryKeyField("ID")]
        Lista_Tipuri_CT_Nou = 440,

        //Id-uri mai mari pentru Anaf
        [EnumDescription("Nomenclator_AnafUM")]
        [PrimaryKeyField("AnafUM_ID")]
        Nomenclator_AnafUM = 10000,

        [EnumDescription("Nomenclator_AnafStocuri_NC8")]
        [PrimaryKeyField("AnafStoc_ID")]
        Nomenclator_AnafStocuri_NC8 = 10001,

        [EnumDescription("Nomenclator AnafAssetTransactionType")]
        [PrimaryKeyField("AssetTransactionType_ID")]
        Nomenclator_AnafAssetTransactionType = 10002,

        [EnumDescription("Nomenclator Anaf TaxType")]
        [PrimaryKeyField("AnafTaxType_ID")]
        Nomenclator_AnafTaxType = 10003,

        [EnumDescription("Nomenclator Anaf Address Type")]
        [PrimaryKeyField("AnafAddressType_ID")]
        Nomenclator_AnafAddressType = 10004,

        [EnumDescription("Nomenclator_AnafStocuri")]
        [PrimaryKeyField("Stocuri_ID")]
        Nomenclator_AnafStocuri = 10005,

        [EnumDescription("Nomenclator_AnafStockChar")]
        [PrimaryKeyField("StockChar_ID")]
        Nomenclator_AnafStockChar = 10006,

        [EnumDescription("Nomenclator_AnafTypeHeaderComment")]
        [PrimaryKeyField("ID")]
        Nomenclator_AnafTypeHeaderComment = 10007,

        [EnumDescription("Nomenclator_AnafAsociereTabHeader")]
        [PrimaryKeyField("ID")]
        Nomenclator_AnafAsociereTabHeader = 10008,
    }

    public enum CommonControlType : int
    {
        Undefined = 0,
        Lista_Valute_Toate = 1,
        Lista_Valute_Nearhivate = 2,
        Lista_Tipuri_Conturi = 3,
        Nomenclator_CentreDeCost = 4,
        Nomenclator_Conturi = 5,
        FE_Furnizor = 6,
        FE_Cumparator = 7,
        Lista_Orase_OrasTara = 8,
        Lista_Orase_OrasTara_Din_Tara = 9,
        Lista_Tari = 10,
        Lista_Tari_Prescurtare = 11,
        Lista_Orase_Toate = 12,
        Lista_Orase_Din_Tara = 13,
        Lista_Judete = 14,
        Lista_Firme_Toate = 15,
        Lista_Serii_FE_Toate = 16,
        Lista_Serii_FE_Pe_Filiala = 17,
        Lista_Angajati = 18,
        Lista_Tip_TVA_Vanzari = 19,
        Lista_Tip_TVA_Cumparari = 20,
        Nomenclator_Centre_Profit = 21,
        Nomenclator_SubcentreDeProfit = 22,
        Lista_Masini_Proprii_Neanulate = 23,
        Lista_Masini_Terti = 24,
        Lista_Servicii_Facturi_Emise = 25,
        Lista_Conturi = 26,
        Lista_Masini_Proprii_Toate = 27,
        Nomenclator_UnitatiMasura = 28,
        Lista_Angajati_Toti = 29,
        Lista_Cota_TVA = 30,
        Lista_Servicii_Facturi_Primite = 31,
        Lista_Clase_Cheltuieli_PA_Toate = 32,
        Lista_Clase_Cheltuieli_Reparatii = 33,
        Lista_Clase_Cheltuieli_Fixe = 34,
        FP_Furnizor = 35,
        FP_Cumparator = 36,
        Lista_Piese = 37,
        Nomenclator_TipRepartizareCosturi = 38,
        Nomenclator_TipRepartizareTVA = 39,
        FP_Caraus = 40,
        Lista_Clasa_CashFlow = 41,
        Lista_Modalitati_Incasari_Plati = 42,
        Firme_Cu_Adresa = 43,
        TipFactura = 44,
        Nomenclator_CC_Management_Facturi_Emise = 45,
        Nomenclator_Documente_Cursa = 46,
        Lista_Masini_Repartizare_Costuri = 47,
        Lista_Marci_Masini = 48,
        Lista_Feluri_Masini = 49,
        Lista_Tipuri_Masini = 50,
        Lista_Soferi_Toti = 51,
        Tip_Cursa = 52,
        Categorii_Firme = 53,
        Lista_Functii = 54,
        Lista_Persoane_Contact = 55,
        Lista_Tipuri_Asigurari = 56,
        Lista_Tipuri_Finantari = 57,
        Interfete = 58,
        Groups = 59,
        Filiale = 60,
        Lista_Case = 61,
        Lista_Conturi_Banca = 62,
        Lista_Persoane_Contact_Din_Firma = 63,
        Tip_Tarifare_Partida = 64,
        Lista_Firme_Toate_Inclusiv_Anulate = 65,
        Lista_Conturi_Inclusiv_Anulate = 66,
        Lista_Angajati_Inclusiv_Anulati = 67,
        Lista_Natura_Marfa = 68,
        Nomenclator_ImpachetareMarfa = 69,
        Nomenclator_Tipuri_Note_Contabile = 70,
        Lista_Firme_Adresa_Livrare = 71,
        Nomenclator_Grupe_Functii = 72,
        Nomenclator_Regimuri_Transport = 73,
        Lista_Curse = 74,
        Lista_Operatiuni_Jurnale = 75,
        Nomenclator_DocBanca_TipDocJustificativ = 76,
        Nomenclator_Intrare_Iesire_Casa_Banca = 77,
        Nomenclator_SubcentreDeProfit_Toate = 78,
        Lista_Categorie_Banca = 79,
        Lista_Categorie_Casa = 80,
        Lista_Deconturi_Casa_Banca = 81,
        Firme_Proprii_Conturi_Pt_Banca_Toate = 82,
        Firme_Proprii_Conturi_Pt_Banca_Active = 83,
        Zilele_Saptamanii = 84,
        Lista_Persoane_Contact_Din_Firma_Pentru_Detalii = 85,
        Adrese_Incarcare_Descarcare = 86,
        Lista_Banci = 87,
        Firme_RuteNegociate = 88,
        Lista_ClaseCheltuieli_Decont_Cursa_Toate = 89,
        Lista_ClaseCheltuieli_Decont_Terti_Toate = 90,
        Lista_ClaseCheltuieli_Toate = 91,
        Lista_ClaseCheltuieli_Neanulate = 92,
        Nomenclator_DocClasaCheltuieli = 93,
        Lista_NrAuto_Remorca_Toate = 94,
        Lista_NrAuto_Remorca_Neanulate = 95,
        Nomenclator_Clase_De_Poluare_Masini = 96,
        Nomenclator_Tipuri_Licente_Masini = 97,
        Nomenclator_Categorii_Masini = 98,
        Lista_NrAuto_Motor_Toate = 99,
        Lista_NrAuto_Motor_Neanulate = 100,
        Lista_ClaseCheltuieli_Reparatii_Toate = 101,
        Lista_ClaseCheltuieli_Fixe_Toate = 102,
        Lista_ClaseCheltuieli_Decont_Cursa_Neanulate = 103,
        Lista_ClaseCheltuieli_Decont_Terti_Neanulate = 104,
        Lista_ClaseCheltuieli_Reparatii_Neanulate = 105,
        Lista_ClaseCheltuieli_Fixe_Neanulate = 106,
        Nomenclator_Titulatura = 107,
        Lista_Produse_Toate = 108,
        Nomenclator_Date_Scadente_Toate = 109,
        Nomenclator_Date_Scadente_Masini = 110,
        Nomenclator_Date_Scadente_Angajati = 111,
        Nomenclator_Tip_Consum = 112,
        Nomenclator_Anotimpuri_Consum = 113,
        Lista_Tip_Card = 114,
        Nomenclator_CarduriMasini = 115,
        Nomenclator_Consumabile_Schimburi_Auto = 116,
        Nomenclator_Marci_Anvelope = 117,
        Nomenclator_Anotimpuri_Anvelope = 118,
        Lista_Nr_Auto_Toate = 119,
        Nomenclator_Profile_Anvelope = 120,
        Nomenclator_CEC_BO = 121,
        Nomenclator_DocBanca_TipDocPlata = 122,
        Nomenclator_DocCasa_TipDocPlata = 123,
        Nomenclator_DocCasa_TipDocJustificativ = 124,
        Lista_Documente_Deconturi = 125,
        Nomenclator_Reparatii_RegieAuto = 126,
        Nomenclator_Tipuri_Daune = 127,
        Nomenclator_TipDoc_Daune = 128,
        Nomenclator_Coeficienti_De_Drum = 129,
        Nomenclator_Tip_Natura_Marfa = 130,
        Nomenclator_Capabilitati_Tip_Marfa = 131,
        Nomenclator_Obiecte_Inventar = 132,
        Lista_Piloti_Toti = 133,
        Lista_Piloti_Neanulati = 134,
        Nomenclator_Date_Scadente_Aeronave = 135,
        Lista_Servicii_Facturi_Emise_Transport_Rutier = 136,
        Lista_Curse_Pentru_Cheltuiala_Cursa = 137,
        Lista_Clienti = 138,
        Lista_Firme_Cu_Adresa_Livrare = 139,
        Lista_Clienti_Toti = 140,
        Lista_Firme_Cu_Adresa_Livrare_Toate = 141,
        Lista_Dispeceri = 142,
        Nomenclator_Tip_Zbor = 143,
        Nomenclator_Categorie_Zbor = 144,
        Lista_Aeronave_Toate = 145,
        Lista_Aeronave_Neanulate = 146,
        Lista_Servicii_Facturi_Emise_Transport_Aerian = 147,
        Nomenclator_Status_Reparatie = 148,
        Nomenclator_TipOperatie_Reparatie = 149,
        Lista_Partide_Curse = 150,
        Lista_Gestiuni = 151,
        Lista_Locatie_Magazie = 152,
        Lista_Comenzi_Clienti = 153,
        Lista_Firma_Proprie_Conturi_Bancare = 154,
        Lista_Angajati_Proprii_Date_BI = 155,
        Lista_Grupe_Produse = 156,
        Lista_Trasee_Prestabilite = 157,
        Lista_Soferi_Curse_Proprii_Toti = 158,
        Lista_Soferi_Curse_Proprii = 159,
        Lista_Angajati_Toti_Si_Alte_Firme = 160,
        Lista_Masini_Proprii_Terti = 161,
        Nomenclator_Scop_Dispozitie = 162,
        Nomenclator_Documente_Gestiune = 163,
        Serie_Carduri_Toate = 164,
        Serie_Carduri_Neanulate = 165,
        Lista_Dosare_Daune = 166,
        Lista_Facturi_Carausi = 167,
        Lista_Societati_Asigurari = 168,
        Lista_NrAuto_Comenzi_Service = 169,
        Comanda_Client_Service = 170,
        Denumire_Operatie_Service = 171,
        Lista_Bon_De_Consum = 172,
        Nomenclator_Card_Produse = 173,
        Comanda_Client_Service_Toate = 174,
        Lista_Preturi_Gestiune = 175,
        Lista_Luni = 176,
        Lista_Soferi_Toti_Si_Alte_Firme = 177,
        Nomenclator_Tip_PV = 178,
        Lista_Servicii_Facturi_Emise_Service = 179,
        Lista_Servicii_Facturi_Emise_Gestiune = 180,
        Lista_Curse_Pentru_Cheltuiala_Cursa_Toate = 181,
        Lista_Curse_Pentru_Foi_De_Parcurs = 182,
        Lista_Curse_Pentru_Foi_De_Parcurs_Toate = 183,
        Nomenclator_Lista_Tipuri_Finantari = 184,
        Lista_Finantari_Nr_Contract = 185,
        Nomenclator_ListaLocatieMagazie = 186,
        Bon_Consum_Masini_Proprii = 187,
        Bon_Consum_Masini_Terti = 188,
        Lista_Firme_Grupuri = 189,
        Lista_NrAuto_Comenzi_Proprii_Service = 190,
        Lista_NrAuto_Comenzi_Terti_Service = 191,
        FP_Furnizor_Achizitie = 192,
        MF_Lista_Tipuri_Amortizari = 193,
        MF_Lista_Grupe = 194,
        MF_Lista_Subgrupe = 195,
        MF_Lista_Stari = 196,
        Nomenclator_Tip_MF = 197,
        Lista_Conturi_Sintetice = 198,
        Carnete_TIR_Comenzi = 199,
        Carnete_TIR_Curse = 200,
        Nomenclator_Tip_Restrictii_Autorizatii = 201,
        Nomenclator_TipExpediere_Registratura = 202,
        Nomenclator_TipDocument_Registratura = 203,
        Nomenclator_Tip_Cazier_Angajati = 204,
        Lista_Curse_Intermediate = 205,
        Lista_Valute_EUR_RON = 206,
        Lista_UM_Baza_Ofertare = 207,
        Nomenclator_UM_Conversii = 208,
        Nomenclator_Tarife_Grupaj = 209,
        Bon_Consum_Gestiune_Atelier = 210,
        Lista_Scadente_Culori = 211,
        Nomenclator_Comercial_Tarife_Autorizatii = 212,
        Nomenclator_Matrici_Tarife_Tari = 213,
        Nomenclator_Marci_Tipuri_Produse = 214,
        Nomenclator_Subgrupe_Produse = 215,
        Nomenclator_Marci_Produse = 216,
        Nomenclator_Tipuri_Produse = 217,
        Nomenclator_Producatori = 218,
        Nomenclator_Coduri_Modele = 219,
        Nomenclator_Mod_Preluare_Comanda_Service = 220,
        Clase_Cash_Flow = 221,
        Lista_Nr_Auto_Semiremorci_Terti = 222,
        Nomenclator_Coduri_Echivalente_Produse = 223,
        Nomenclator_Tip_Comanda_Service = 224,
        Nomenclator_Echivalente_Produse = 225,
        Lista_Devize_Estimative_Service = 226,
        Lista_Devize_Estimative_Detalii_Service = 227,
        Nomenclator_Grupe_Obiecte_De_Inventar = 228,
        Lista_Scadente_Perioade_Culori = 229,
        Nomenclator_Sectii_Service = 230,
        Nomenclator_Tip_Masini_Operatii_Service = 231,
        TurRetur = 232,
        Places = 233,
        Lista_Gestiuni_Dunca = 234,
        Comanda_Client_Service_Devize = 235,
        Lista_Rampe_Service = 236,
        Lista_Tip_Lucrari_Service = 237,
        Lista_Masini_Terti_Corecta = 238,
        Lista_Persoane_Din_Service = 239,
        Lista_Cod_Curse_Toate = 240,
        Lista_Masini_Proprii_Neanulate_Dunca = 241,
        Lista_NrAuto_Remorca_Toate_Dunca = 242,
        Lista_NrAuto_Remorca_Neanulate_Dunca = 243,
        Lista_NrAuto_Motor_Toate_Dunca = 244,
        Lista_NrAuto_Motor_Neanulate_Dunca = 245,
        Bon_Consum_Masini_Proprii_Dunca = 246,
        Serie_Carduri_Toate_Dunca = 247,
        Serie_Carduri_Neanulate_Dunca = 248,
        Lista_Produse_Toate_Viotrans = 249,
        Lista_Curse_Pentru_Cheltuiala_Cursa_DUNCA = 250,
        Lista_Curse_Pentru_Cheltuiala_Cursa_Toate_DUNCA = 251,
        Lista_Curse_Pentru_Foi_De_Parcurs_DUNCA = 252,
        Lista_Curse_Pentru_Foi_De_Parcurs_Toate_DUNCA = 253,
        Lista_Curse_Cu_Centre_Subcentre = 254,
        Lista_Produse_Toate_Coduri_Referinta_Viotrans = 255,
        Lista_Persoane_Contact_Din_Firma_Toti = 256,
        Nomenclator_Tip_Cursa = 257,
        Lista_CoduriEchivalente_Radacini = 258,
        Lista_Tip_Transporturi = 259,
        Lista_Matrice_Transporturi = 260,
        Calatori_Grupe_Traseu = 261,
        Calatori_Traseu = 262,
        Calatori_Operatiuni_Traseu = 263,
        Calatori_Foaie_Parcurs = 264,
        Calatori_Ghid = 265,
        Calatori_Lista_Tip_Soferi = 266,
        Lista_NrAuto_NaturaMarfa = 267,
        Nomenclator_Tip_Lucrare_Comanda_Service = 268,
        Nomenclator_Juridic_Instante_De_Judecata = 269,
        Nomenclator_Juridic_Natura_Litigii = 270,
        Nomenclator_Juridic_Faze_Procesuale = 271,
        Nomenclator_Aeroporturi = 272,
        Nomenclator_Clase_ADR = 273,
        Nomenclator_Conditii_De_livrare = 274,
        Nomenclator_Departamente = 275,
        Nomenclator_Tip_Lot_Marfa = 276,
        Nomenclator_Linii_Grupaj = 277,
        Nomenclator_Porturi = 278,
        Nomenclator_Terminale = 279,
        Nomenclator_Statusuri_Lot_Marfa = 280,
        Nomenclator_Tipuri_Container = 281,
        Lista_Curse_Pentru_Cheltuiala_Cursa_VIOTRANS = 282,
        Lista_Curse_Pentru_Cheltuiala_Cursa_Toate_VIOTRANS = 283,
        Nomenclator_Matrice_TVA = 284,
        Nomenclator_Tip_Tari = 285,
        Nomenclator_Lot_Marfa = 286,
        Nomenclator_Locuri_Montare_Anvelope = 287,
        Lista_NrAuto_Motor_Toate_Calatori = 288,
        Lista_NrAuto_Motor_Neanulate_Calatori = 289,
        Lista_Soferi_Curse_Proprii_Toti_Calatori = 290,
        Lista_Soferi_Curse_Proprii_Calatori = 291,
        Lista_Angajati_Inclusiv_Anulati_Vectra = 292,
        Lista_Angajati_Vectra = 293,
        Lista_Loturi_Marfa = 294,
        Nomenclator_Tip_Dispozitie = 295,
        Nomenclator_Lot_Marfa_Subcentre_Cost = 296,
        Nomenclator_Tip_Camioane_Carausi = 297,
        Lista_Coduri_Aeriene = 298,
        Lista_Coduri_Maritime = 299,
        Nomenclator_Tip_Nonconformitate = 300,
        Lista_Fel_Transport = 301,
        Nomenclator_Documente_Vamale = 302,
        Nomenclator_Facilitati_Vama = 303,
        Nomenclator_Operatiuni_Vama = 304,
        Nomenclator_Juridic_Definitiva_Irevocabila_Investire = 305,
        Lista_Juridic_Firme_Toate_Inclusiv_Anulate = 306,
        Lista_Somatii = 307,
        Lista_Juridic_Executori = 308,
        Lista_Juridic_Debitori = 309,
        Lista_Contracte_Munca = 310,
        Lista_Mecanici_Toti = 311,
        FAZ_Preluare_Litrii_Consumati = 312,
        FAZ_Preluare_Litrii_Consumati_Termoking = 313,
        Lista_Utilizatori = 314,
        Lista_Clienti_Cu_Comenzi_Transport = 315,
        Lista_Adrese_Firme_Marfuri_Integrate = 316,
        Lista_Solicitari = 317,
        Lista_Masini_Repartizare_Costuri_Inclusiv_Radiate = 318,
        Nomenclator_Zone_Expeditie = 319,
        Nomenclator_Tip_Tarife_Expeditie = 320,
        Nomenclator_Producatori_Echivalenti = 321,
        Calatori_Lista_Venituri = 322,
        Lista_Produse_Toate_NoPoup = 323,
        Lista_Natura_MarfaClasaADR = 324,
        Lista_Angajati_Exclus_Anulati_Vectra = 325,
        Lista_Coduri_Marfa_Loturi_Integrate_Cursa = 326,
        Deviz_Estimativ_Comenzi_Client_Neasignate = 327,
        Lista_Firme_Toate_DKV = 328,
        Nomenclator_Tipuri_Tarifare_Rutier = 329,
        Nomenclator_Tipuri_Documente_Financiare = 330,
        Nomenclator_Departamente_Expeditii = 331,
        Lista_Soferi_Curse_Proprii_Vectra = 332,
        Lista_Orase_OrasTaraMM = 333,
        Lista_Operatii_GE = 334,
        Lista_Expeditii_Servicii_Subcentre = 335,
        Lista_Clasa_CashFlow_Expeditii = 336,
        Lista_Servicii_Facturi_Emise_Transport_Maritim = 337,
        Lista_Servicii_Facturi_Primite_Rutier = 338,
        Lista_Servicii_Facturi_Primite_Aerian = 339,
        Lista_Servicii_Facturi_Primite_Maritim = 340,
        Lista_Clasa_CashFlow_General = 341,
        Nomenclator_Grupe_Adaos_Produse = 342,
        Lista_Firme_Toate_DKV_Filtrate = 343,
        Lista_Firme_Cu_Adresa_Livrare_Toate_Filtrate = 344,
        Lista_Comenzi_Clienti_Bon_Transfer = 345,
        Lista_Comenzi_Clienti_Bon_Consum = 346,
        Lista_Orase_OrasTara_Filtrate = 347,
        Lista_Curse_Pentru_Cheltuiala_Cursa_ALEX = 348,
        Lista_Curse_Pentru_Cheltuiala_Cursa_Toate_ALEX = 349,
        Lista_Orase_OrasTaraMM_Filtrate = 350,
        Lista_Valute_Operatiuni_Expeditii = 351,
        Nomenclator_Tancuri_Combustibil = 352,
        FP_Furnizor_EXpeditii = 358,
        Lista_Soferi_Curse_Decont = 450,
        Nomenclator_Ruta_Comenzi = 526,

        #region Logs

        WindnetModules = 353,
        WindnetUserInterfaces = 354,
        WindnetOperationTypes = 355,
        WindnetRecordStates = 356,
        WindnetExecutionResults = 357,

        #endregion Logs

        Nomenclator_Date_Scadente_Firme = 359,
        Lista_Discount_Manopera_Service = 360,
        Lista_Discount_Produse_Service = 361,
        Lista_Comanenzi_Ferry = 362,
        Lista_Piese_Dunca = 363,
        FE_Cumparator_Alex_Generari_Facturi = 364,
        Nomenclator_Subdepartamente = 365,
        Nomenclator_Departamente_Existente = 366,
        Lista_Servicii_Facturi_Emise_Pe_Filiale = 367,
        Lista_Firme_Discount_Piese = 368,
        Lista_Servicii_Facturi_Emise_Vama_Pe_Filiale = 369,
        Nomenclator_Rute_Comenzi_Ferry = 370,
        Nomenclator_Auto_Comenzi_Ferry = 371,
        Nomenclator_Semiremorca_Comenzi_Ferry = 372,
        Nomenclator_Tipuri_Tarifare_Rutier_Filtrat = 373,
        Nomenclator_Tipuri_Documente_Financiare_Filtrat = 374,
        Nomenclator_Departamente_Registratura = 375,
        Nomenclator_Linii_Grupaj_Toate = 376,
        Lista_Servicii_Facturi_Emise_Pe_Filiale_AFN = 377,
        Lista_Servicii_Facturi_Primite_AFN = 378,
        Nomenclator_Tipuri_Clienti_Furnizori = 379,
        Calatori_Traseu_Pontaj = 380,
        Lista_Devize_Estimative_Viotrans = 381,
        Calatori_Lista_Operatiuni_Programator = 382,
        Nomenclator_EDI_Scan = 383,
        Nomenclator_EDI_Scan_Points = 384,
        Nomenclator_EDI_Status_Reports = 385,
        Lista_Produse_Licitatii_Dunca = 386,
        Nomenclator_Dimensiuni_Ferry = 387,
        Lista_Linii_Aeriene = 388,
        Lista_Servicii_Aerian = 389,
        Lista_Servicii_Maritim = 390,
        Lista_Servicii_Linii_Aerian = 391,
        Lista_Tipuri_Tarifare_Aerian_Maritim = 392,
        Lista_Servicii_Facturi_Emise_Transport_Depozit = 393,
        Lista_Preturi_Rute_Speciale_Ferry = 394,
        Nomenclator_Rute_Comenzi_Ferry_Speciale = 395,
        FilialeFiltrat = 396,
        Lista_ObiecteInventar = 397,
        Nomenclator_Tip_Concedii = 398,
        Lista_Masini_Proprii_Toate_Dunca = 399,
        Lista_Contracte_Leasing = 400,
        Nomenclator_Clase_EBIDTA = 401,
        Nomenclator_Tipuri_Servicii_Solicitari = 402,
        ListaCurseTur = 403,
        Lista_Servicii_Facturi_Emise_Vama = 404,
        Lista_Persoane_Contact_Firma_Proprie = 405,
        Nomenclator_UnitatiMasuraAerian = 406,
        Nomenclator_ImpachetareMarfaAerian = 407,
        Lista_Mecanici_Grupa = 408,
        Lista_Linii_Maritime = 409,
        Lista_Servicii_Linii_Maritim = 410,
        Nomenclator_DimensiuniVehicul = 411,
        Nomenclator_Anexa_Comanda = 412,
        FP_Furnizor_Dunca = 413,
        Lista_Centre_Profit_Filtrate_Aigle = 414,
        Nomenclator_Users_All_Rights = 415,
        Nomenclator_Stadii_Dosar = 416,
        Lista_Produse_Toate_ChimTotal = 417,
        Lista_Mecanici_Neanulati = 418,
        Nomenclator_Tip_Revizii = 419,
        Nomenclator_Tip_Operatiuni_Service = 420,
        Lista_Angajati_Pontaj_Mecanici_ChimTotal = 421,
        Lista_Angajati_Preavizare = 422,
        Lista_CHGSCode = 423,
        Lista_Firme_Facturabile = 424,
        Nomenclator_SCI = 425,
        Nomenclator_Durata_Contract = 426,
        Nomenclator_Norme_CIM = 427,
        Nomenclator_Tip_Tarifare_Operatii_Service = 428,
        Denumire_Operatie_Service_Dunca = 429,
        RapoarteSituatiiContabile = 430,
        Lista_Clasa_CashFlow_Venit = 431,
        Lista_ContracteIndividualeMunca = 432,
        ListaProduseFisaDeMagazie = 433,
        ListaGestiuniFisaDeMagazie = 434,
        ListaConturiFisaDeMagazie = 435,
        ListaAngajatiCuNotificari = 436,
        Users_All_Rights = 437,
        Nomenclator_Statusuri_Depozit = 438,
        Users = 439,
        ManagerFiliale = 440,
        Nomenclator_Indicatori_Forecast = 441,
        Calatori_Lista_Rute_International = 442,
        Lista_Dispeceri_Marvi = 443,
        Lista_Servicii = 444,
        Lista_Masini_Proprii_Toate_Centre = 445,
        Lista_Masini_Proprii_Neanulate_Centre = 446,
        Nomenclator_Categorii_Note_Adiacente = 447,
        Lista_Conturi_Nedeductibile = 448,
        Nomenclator_Tip_Cheltuiala_Deductibila = 449,
        Lista_Tarife_Aerian = 451,
        Nomenclator_CRM = 452,
        Nomenclator_TipAbsenta = 453,
        Lista_Curse_Pentru_Cheltuiala_Cursa_Filtrat = 454,
        Nomenclator_Tip_GPS = 455,
        Lista_Firme_Toate_Inclusiv_Anulate_Filtrate = 456,
        Nomenclator_CRM_TransportType = 457,
        Nomenclator_CRM_IncarcariType = 458,
        Nomenclator_CRM_CondLiv = 459,
        Lista_Decont = 460,
        Nomenclator_Oferte_Directie = 461,
        Nomenclator_Oferte_Tip_Serviciu = 462,
        Nomenclator_Tipuri_Servicii_Oferte = 463,
        Lista_Comenzi_Clienti_Bon_Consum_Generat = 464,
        Lista_Angajati_Proces_Verbal_PP = 465,
        Lista_Carduri_Pe_Tip_Proces_Verbal_PP = 466,
        Lista_Carduri_Altele_Proces_Verbal_PP = 467,
        Lista_Masini_Proces_Verbal_PP = 468,
        Calatori_Serie_Interbus = 469,
        Lista_Proces_Verbal_Constatare = 470,
        Nomenclator_Echivalente_Produse_Fisa_Magazie = 471,
        Calatori_Nomenclator_Destinatie = 472,
        Calatori_Nomenclator_Tronsoane = 473,
        Lista_NrDosare_Daune = 474,
        Lista_Angajati_Functie = 475,
        Bilant_Nomenclator_Tipuri_Formular = 476,
        Bilant_Nomenclator_Clasificari = 477,
        Bilant_Formule_Calcul = 478,
        Lista_Tip_Referat = 479,
        Lista_Cauze_Intarziere = 480,
        Nomenclator_Tip_Cutie_Viteze = 481,
        Lista_UM = 502,
        Lista_Sursa_Energie = 503,
        Lisa_Comenzi_Curse = 504,


        #region Management anvelope

        ListaTipAutoAnvelope = 500,
        TipOperatiuniAnvelope = 501,

        #endregion
        Lista_Conturi_Nedeductibile_TransportePopovici = 505,
        Lista_Conturi_Nedeductibile_Inclusiv_Anulate_TransportePopovici = 506,
        Lista_Gestiuni_FaraPieseReconditionate = 507,
        Lista_Angajati_Exclus_Anulati_KSC = 508,
        Lista_Tip_Consum = 509,
        Nomenclator_Tip_Consum_TK = 510,
        Nomenclator_Diurna_Sofer = 511,
        MF_Tip_ProcesVerbal = 512,
        Status_Soferi_Cursa = 513,
        Nomenclator_Status_Soferi_Curse = 514,
        FacturiDaune = 515,
        Lista_NrAuto_ImportCombustibil = 516,
        Lista_Conturi_Egalizare = 517,
        Nomenclator_Tipuri_Agregat = 518,
        Nomenclator_Card_Produse_Combustibil = 519,
        Nomenclator_Tip_Raport_EBIDTA = 520,
        Nomenclator_Indicatori_EBIDTA = 523,
        Lista_Bon_Transfer_Avize = 524,

        #region Export date
        Lista_TipExport = 521,
        Lista_Subcentre_Masini = 522,
        Nomenclator_Parametrii = 525,
        #endregion



        Lista_Produse_Combustibil = 527,
        Lista_Intervale = 528,
        Nomenclator_Rapoarte = 529,
        Lista_Servicii_Bon_Fiscal = 530,
        Nomenclator_Consum_Suplimentar = 531,
        Lista_Conturi_Modele_Contare = 532,
        Lista_Masini_Terti_Caraus = 533,
        Nomenclator_Tip_Documente = 534,
        Nomenclator_Departamente_Toate = 535,


        #region TransporturiTrust (doar pentru Dolotrans)
        Nomenclator_Marci_Vehicule = 536,
        Nomenclator_Tipuri_Caroserie = 537,
        #endregion TransporturiTrust (doar pentru Dolotrans)

        Nomenclator_TipImport = 538,
        Denumire_Operatie = 539,
        Nomenclator_Flote = 540,
        Nomenclator_Categorie_Tonaj = 541,
        Nomenclator_Motiv_Indisponibilitate = 542,
        Nomenclator_DiurnaXTara = 543,
        Nomenclator_Tip_Tronson = 544,
        Lista_Curse_Pentru_Foi_De_Parcurs_Filtrat = 545,
        BC_Combustibil_Preluare_Litrii_Consumati = 546,
        SerieAvize = 547,
        SerieChitante = 548,
        Nomenclator_Tipuri_Rapoarte = 549,
        Nomenclator_Panouri = 550,

        #region ControlSelectieLookup
        ListaPersoaneProprii = 551,
        ListaPersoaneTerti = 552,
        ListaMasiniProprii = 553,
        ListaMasiniTerti = 554,
        ListaCapTractoareProprii = 555,
        ListaCapTractoareTerti = 556,
        ListaSemiremorciProprii = 557,
        ListaSemiremorciTerti = 558,
        ListaFirme = 559,
        ListaCursaPartideIntegrate = 560,
        ListaCurseDecont = 561,
        ListaCurse = 562,
        Lista_Partide_Curse_Filtrat = 563,
        Lista_Angajati_Proprii_Persoana_Fizica = 564,
        Lista_FacturareCursa = 565,
        #endregion ControlSelectieLookup

        //S-au dat Id-uri mari pentru a nu intra in conflict 
        ListaCodTaxareInversa = 5555,
        ListaCodCAEN = 5556,

        Nomenclator_Validari = 651,
        NomenclatorClasificariPlanificare = 652,
        Nomenclator_Coloane_Planificator = 653,
        Nomenclator_Clasificari_Planificare = 654,
        Nomenclator_Tipuri_Culori_Planificare_1 = 655,
        Nomenclator_Tipuri_Culori_Planificare_2 = 656,
        Nomenclator_Statusuri_Cursa = 657,
        Nomenclator_Responsabilitati = 658,
        Nomenclator_Cauze = 659,
        Nomenclator_MotiveIncetare_CIM = 660,
        Lista_ActAditional_ElementeCIM = 661,
        Nomenclator_TipPersoana = 662,
        Lista_Angajati_ContracteCIM = 663,
        Nomenclator_Tip_Retineri_Salarii = 664,
        Lista_Angajati_CategoriiConcedii = 665,
        Lista_Angajati_TipConcedii = 666,
        Nomenclator_Initiator_Poprire = 667,
        Lista_Produse_Autovehicule = 668,
        Coduri_Statii_Shell = 669,
        Lista_Orase_Cod = 670,
        Lista_Conturi_Modele_Contare_Inclusiv_Anulate = 671,
        Nomenclator_Tip_Rate_Asigurari = 672,
        Nomenclator_Tip_Marfa_Depozit = 673,
        Nomenclator_Produse_Depozit = 674,
        Nomenclator_Depozite = 675,
        Nomenclator_Tip_Calculator_Cisterna = 676,
        Nomenclator_Coduri_Cisterna = 677,
        Nomenclator_Corp_Masurator_Cisterna = 678,
        Nomenclator_Produse_Transportate = 679,
        Nomenclator_Tip_Contracte = 680,
        Nomenclator_TipDocPlata = 681,
        Nomenclator_Indicatori_EBIDTA_Formule_Calcul_CondCont = 682,
        Lista_NrAuto_ImportCombustibil_Neradiate = 683,
        Firme_Cu_Adresa_Fara_Persoana_Fizica = 684,
        Lista_Masini_Detalii = 685,
        FE_FurnizorCont = 686,
        Nomenclator_Tip_Splitare_Comanda = 687,
        Nomenclator_Tarife_Transport = 688,
        Nomenclator_Tipuri_RapoartePopUp = 689,

        Nomenclator_Statusuri_Solicitari = 690,
        NomenclatorMotiveRefuz = 691,
        Nomenclator_Tip_Finalizare_Oferta = 692,
        Panouri_Scadente_Users = 693,
        Panouri_Scadente = 694,
        Lista_NrAutoTerti_ImportCombustibil = 695,
        Contracte = 696,
        Nomenclator_Diurna_DecontTerti = 697,
        Nomenclator_TipRepartizareVenituri = 698,
        Nomenclator_Tip_Amenda = 699,
        Nomenclator_abateri_disciplinare = 700,
        Lista_Categorie_Casa_InchidereDecont = 701,
        Nomenclator_Tip_Documente_Renumerotare = 702,
        Nomenclator_TipPrioritate = 703,

        Lista_Mecanici_Toti_Functii = 704,
        Nomenclator_Status_Partide = 705,
        PlanificareStatusIncarcariDescarcari = 706,
        Lista_Producatori = 707,
        ListaAdreseLivrare = 708,
        Lista_Firme_Cu_Adresa_Livrare_Filtrate = 709,
        Lista_FurnizoriCombustibil = 710,
        Lista_CarduriMasiniCuIstoric_ImportCombustibil = 711,
        PlanificatorSoferi = 712,
        PlanificatorMasini = 713,
        Lista_Dispeceri_Neanulati = 714,
        Lista_GestiuniPerUser = 715,
        Lista_Useri = 716,
        Lista_Adrese_Livrare_Spalatorie = 717,
        Nomenclator_TipComanda = 718,
        Lista_Conturi_Factoring = 719,
        Lista_Facturi_Neincasate = 720,
        Lista_Facturi_NePlatite = 721,
        Nomenclator_ExportDate = 722,
        Nomenclator_Puncte_Service = 723,
        Nomenclator_Tipuri_Apeluri = 724,
        Lista_Raport_Eveniment = 725,
        Nomenclator_MotivIncetareContract = 726,
        Nomenclator_Sabloane_Facturi =727,
        Nomenclator_Furnizor_Import_Comenzi_GPS = 728,
        ListaCarduri = 729,
        Nomenclator_ModalitatePlataSpalatorii = 730,
        Nomenclator_ExportDateMain = 731,
        Nomenclator_TipExportDateMain = 732,
        Nomenclator_X_ExportDateMain = 733,
        Nomenclator_CosturiCursa_Partide = 734,
        Nomenclator_TipPauza = 735,
        Lista_Servicii_Facturi_Emise_Contracte = 736,
        Firme_Cu_Adresa_Cu_Persoana_Fizica = 737,
        FP_Furnizor_Toti = 738,
        Nomenclator_TipulDocumentuluiComunicat = 739,
        Lista_Conturi_Client = 740,
        Lista_Conturi_Furnizor = 741,

        Lista_Tip_TVA_Vanzari_Filtered = 742,
        Lista_Tip_TVA_Cumparari_Filtered = 743,
        Lista_Tip_TVA_Vanzari_Full = 744,
        Lista_Tip_TVA_Cumparari_Full = 745,

        Lista_Conturi_Banca_Toate = 746,
        Lista_Case_Toate = 747,
        Lista_Conturi_Modele_Contare_Inclusiv_Anulate_Parinte = 748,
        Lista_Produse_Facturi_Primite_Achizitie = 749,
        Lista_Conturi_Toate = 750,
        Firme_Proprii_Conturi_Pt_Banca_Facturat = 751,
        ListaPersoanePropriiToti = 752,
        Lista_Orase_OrasTara_Splitare = 753,
        Nomenclator_Vama = 754,
        Nomenclator_ComisionarVamal = 755,
        Nomenclator_ClauzePerioadaCalcul = 756,
        Nomenclator_ClauzeModCalcul = 757,
        Lista_Firme_Toate_GrupIA = 758,
        Nomenclator_StareContract = 759,

        Expresii_Clauza_Diesel_Type1 = 760,
        Expresii_Clauza_Diesel_Type2 = 761,
        ContracteFiltrate = 762,
        Lista_Nr_Auto_Active = 763,
        Bon_Consum_Masini_Proprii_Neanulate = 764,
        Bon_Consum_Masini_Proprii_Dunca_Neanulate = 765,
        Lista_Firme_Toate_Short = 766,
        Lista_Tipuri_CT_Nou = 767,
        ListaClienti_MasiniAsociate = 768,

        //ID-uri mai mari pt Anaf/Saft
        Nomenclator_AnafUM = 10000,
        Nomenclator_AnafStocuri_NC8 = 10001,
        Nomenclator_AnafAchizitiiToate = 10002,
        Nomenclator_AnafLivrariToate = 10003,
        Nomenclator_Anaf_AchizitiiLivrari = 10004,
        Nomenclator_AnafAssetTransactionType = 10005,
        Nomenclator_AnafTaxType = 10006,
        Nomenclator_AnafAddressType = 10007,
        Nomenclator_AnafStocuri = 10008,
        Nomenclator_AnafStockChar = 10009,
        Nomenclator_AnafLivrari = 10010,
        Nomenclator_AnafAchizitii = 10011,
        Nomenclator_AnafTaxImpozite = 10012,
        Nomenclator_AnafWHT = 10013,
        Nomenclator_AnafTypeHeaderComment = 10014,
        Nomenclator_AnafAsociereTabHeader = 10015,
        Lista_AnafTab = 10016,
        Nomenclator_AnafTipuriFacturi = 10017,
        Nomenclator_AnafTVA_NoteContabile = 10018,
        Nomenclator_AnafTipInstrumentPlata = 10019,
    }

    #region Nomenclatoare - tipuri selectie

    public enum Lista_Conturi_TipConturi
    {
        Casa,
        Banca,
        CasaBanca,
        Sintetice,
        Toate,
        Cheltuieli,
        ToateFaraCheltuieli
    }

    public enum Lista_Servicii_Facturi_TipFacturi
    {
        FacturaEmisa,
        FacturaPrimita
    }

    public enum Firme_Persoane_Contact_SelectieFunctie
    {
        Sofer,
        Dispecer,
        Manipulant,
        Toti
    }

    public enum Lista_NrAuto_Masini_Proprii_Tip
    {
        Toate,
        ExcludeRemorcile,
        Remorcile
    }

    public enum Lista_NrAuto_Masini_Proprii_SelectieCampuri
    {
        DetaliiGrilaConsum,
        DetaliiGrilaConsumTermoking,
        DetaliiVolum,
        NiciUna,
        Toate
    }

    #endregion Nomenclatoare - tipuri selectie

    [AttributeUsage(AttributeTargets.Field)]

    public class EnumDescriptionAttribute : Attribute
    {
        private string _text = "";

        /// <summary>

        /// Text describing the enum value

        /// </summary>

        public string Text
        {
            get { return this._text; }
        }

        /// <summary>

        /// Instantiates the EnumDescriptionAttribute object

        /// </summary>

        /// <param name=”text”>Description of the enum value</param>

        public EnumDescriptionAttribute(string text)
        {
            _text = text;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]

    public class PrimaryKeyFieldAttribute : Attribute
    {
        private string _text = "";

        /// <summary>

        /// Text describing the enum value

        /// </summary>

        public string Text
        {
            get { return this._text; }
        }

        /// <summary>

        /// Instantiates the EnumDescriptionAttribute object

        /// </summary>

        /// <param name=”text”>Description of the enum value</param>

        public PrimaryKeyFieldAttribute(string text)
        {
            _text = text;
        }
    }

    public class Enums
    {
        static StringDictionary _enumDescriptions = new StringDictionary();
        static StringDictionary _primaryKeyFields = new StringDictionary();

        public static string GetEnumDescription<EnumType>(EnumType @enum)
        {
            Type enumType = @enum.GetType();

            string key = enumType.ToString() + "___" + @enum.ToString();

            if (_enumDescriptions[key] == null)
            {
                FieldInfo info = enumType.GetField(@enum.ToString());

                if (info != null)
                {
                    EnumDescriptionAttribute[] attributes = (EnumDescriptionAttribute[])info.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);

                    if (attributes != null && attributes.Length > 0)
                    {
                        _enumDescriptions[key] = attributes[0].Text;

                        return _enumDescriptions[key];
                    }
                }

                _enumDescriptions[key] = @enum.ToString();
            }

            return _enumDescriptions[key];
        }

        public static string GetPrimaryKeyField<EnumType>(EnumType @enum)
        {
            Type enumType = @enum.GetType();

            string key = enumType.ToString() + "___" + @enum.ToString();

            if (_primaryKeyFields[key] == null)
            {
                FieldInfo info = enumType.GetField(@enum.ToString());

                if (info != null)
                {
                    PrimaryKeyFieldAttribute[] attributes = (PrimaryKeyFieldAttribute[])info.GetCustomAttributes(typeof(PrimaryKeyFieldAttribute), false);

                    if (attributes != null && attributes.Length > 0)
                    {
                        _primaryKeyFields[key] = attributes[0].Text;

                        return _primaryKeyFields[key];
                    }
                }

                _primaryKeyFields[key] = @enum.ToString();
            }

            return _primaryKeyFields[key];
        }
    }
}
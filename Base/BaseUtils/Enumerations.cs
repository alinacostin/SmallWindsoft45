using Base.BaseUtils;

namespace BaseUtils
{
    public enum ReportType : int
    { 
        Undefined = 0,
        WebReport = -1,
        Factura_Emisa_Detaliat = 1,
        Factura_Emisa_Cumulat = 2,
        Invoice_Detaliat = 3,
        Invoice_Cumulat = 4,
        Anexa_Factura = 5,
        Factura_Emisa_Altele = 6,
        Chitanta = 7,
        Cheltuieli_Parc_Auto_Vizualizare = 8,
        Plati_FisaFurnizor = 9,
        Registru_Casa = 10,
        Centralizare_Casa = 11,
        Registru_Banca = 12,
        Centralizare_Banca = 13,
        Daune = 14,
        AngajatiConcedii_R = 15,
        AngajatiConcedii_D = 16,
        AngajatiConcedii_E = 17,
        AngajatiConcedii_H = 18,
        FacturaDianthus = 19,
        FacturaDianthusCumulat = 20,
        AngajatiConcedii_ML = 21,
        Comanda_Caraus = 22,
        Confirmare_Client = 23,
        Dispozitii_Plata_Incasare = 24,
        AngajatiConcedii_I = 25,
        AngajatiConcedii_F = 26,
        AngajatiConcedii_S = 27,
        FacturaDunca = 28,
        Comanda_Client_Service = 29,
        FacturaVectra = 30,
        Deviz_Service = 31,
        Jurnal_Vanzari_Impozabile = 32,
        Jurnal_Vanzari_Neimpozabile = 33,
        Jurnal_Cumparari_Din_Tara_Extracomunitar = 34,
        Jurnal_Cumparari_Intracomunitare = 35,
        Jurnal_Cumparari_Plata_TVA = 36,
        Jurnal_Vanzari = 37,
        Jurnal_Cumparari = 38,
        Stoc_Marfa = 39, // nu e facut
        NIR = 40,
        Bon_Consum = 41,
        Bon_Consum_Client = 42,
        Bon_Transfer = 43,
        FacturaComertDianthus = 44,
        FacturaDianthusAltele = 45,
        Registru_Jurnal_Analitic = 46,
        Registru_Jurnal_Sintetic = 47,
        Fisa_Magazie = 48,
        Jurnal_Intrari_Iesiri = 49, // nu e facut
        Registru_Jurnal_Analitic_PeNote = 50,
        Registru_Jurnal_Sintetic_PeNote = 51,
        Centralizator_Facturi_Emise = 52,
        Centralizator_Facturi_Primite = 53,
        Centralizator_Facturi_Emise_Scadente = 54, // nu e facut
        Centralizator_Facturi_Primite_Scadente = 55, // nu e facut
        Fisa_De_Urmarire = 56,
        Raport_Managerial_Cheltuieli = 57,
        Raport_Contabilitate_Cheltuieli = 58,// nu e facut
        Fisa_Furnizor = 59,
        Fisa_Client = 60,
        Facturi_Neincasate = 61,
        Facturi_Neplatite = 62,
        Decont = 63,
        FacturaVectraCumulat = 64,
        Fisa_Pe_Persoane = 65,
        Fisa_Pe_Persoane_Toate = 66,
        Fisa_Pe_Persoane_Toate_Pe_Valute = 67,
        Solduri_Clienti_Furnizori = 68,
        Solduri_Clienti_Furnizori_Centralizat = 69,// nu e facut
        FacturaAlexService = 70,
        FacturaAlexCumulatService = 71,
        Comanda_Client_Dunca_Service = 72,
        FacturaEmisaAnexaAlex = 73,
        Comanda_Caraus_Vectra = 74,
        MF_Fisa = 75,
        MF_Inventar = 76,
        Obiecte_Inventar = 77,
        MF_Evidenta = 78,
        Obiecte_Inventar_Evidenta = 79,
        Centralizator_Casa_Banca = 80,
        Centralizator_Conturi = 81,
        Balanta_Analitica_6_Coloane = 82,
        Balanta_Analitica_4_Coloane = 83,
        Balanta_Sintetica_6_Coloane = 84,
        Balanta_Sintetica_4_Coloane = 85,
        Fisa_Contului = 86,
        Fisa_Contului_Pe_Valute = 87,
        Balanta_Analitica_6_Coloane_Cumulat = 88,
        //Balanta_Analitica_Patru_Coloane_Detaliata_RulajCumulat = 89,
        //Balanta_Analitica_Patru_Coloane_Cumulata_RulajDetaliat = 90,
        Balanta_Analitica_Patru_Coloane_Cumulata_RulajCumulat = 91,
        Comanda_Caraus_MM_Otopeni = 92,
        FacturaEmisaMM = 93,
        Deviz_Service_Dunca = 94,
        Angajati_Cazier = 95,
        Bon_Consum_Vectra_Raport = 96,
        Comanda_Caraus_Viotrans = 97,
        Comanda_Caraus_Intern_Viotrans = 98,
        FacturaVioTrans = 99,
        PVC = 100,
        Sold_General_Rentabilitate_Firma = 101,
        Produse_BarCode_Raport = 102,
        Profit_Loss = 103,
        Inventar = 104,
        Adeverinta_Salariat_Raport = 105,
        Deviz_Service_Proprii_Dunca = 106,
        Deviz_Service_Terti_Dunca = 107,
        PV_Transfer_Gestiune = 108,
        Adeverinta_Raport = 109,
        InvoiceVioTrans = 110,
        //Deviz_VioTrans_Raport = 111,
        Deviz_Estimativ_VioTrans_Raport = 112,
        Comanda_Client_Viotrans_Service = 113,
        Comanda_Caraus_Kronospan_Viotrans = 114,
        Bon_Consum_View_Raport = 115,
        FacturaCumulatVioTrans = 116,
        Centralizator_Anvelope_Raport = 117,
        FacturaAnexaVioTrans = 118,
        Balanta_Mijloace_Fixe = 119,
        Dispozitie_Incasare_Plata_View = 120,
        FacturaEmisaAigle = 121,
        FacturaEmisaAigleCumulat = 122,
        Comanda_Caraus_Dunca_Ro = 123,
        Comanda_Caraus_Dunca_EN = 124,
        FacturaEmisaFiladelfia = 125,
        Comanda_Caraus_Filadelfia = 126,
        Comanda_Caraus_Dunca_Subcontractori = 127,
        Comanda_Caraus_Aigle_RO = 128,
        Comanda_Caraus_Aigle_EN = 129,
        Comanda_Caraus_Aigle_Michelin = 130,
        Comanda_Caraus_Aigle_Valeo = 131,
        Comanda_Caraus_Aigle_France = 132,
        Comanda_Caraus_Aigle_Faurecia = 133,
        Foi_Parcurs_Tabita = 134,
        Calatori_Programare_Soferi = 135,
        Comanda_Caraus_Expeditii_Extern_MM = 136,
        Comanda_Caraus_Expeditii_Intern_MM = 137,
        Comanda_Caraus_Expeditii_Extern_MM_EN = 138,
        Comanda_Caraus_Expeditii_Delamode = 139,
        Comanda_LoadingList_MM = 140,
        Aviz_Insotire_Marfa_Delamode = 141,
        Comanda_ListaIncarcare_MM = 142,
        Comanda_ListDescarcare_MM = 143,
        Comanda_WarehouseAviso_MM = 144,
        Foi_Parcurs_AZ = 145,
        Jurnal_Centralizator_DocumenteEmise = 146,
        Jurnal_Centralizator_DocumentePrimite = 147,
        Centralizator_FAZ_Curse_Regulate = 148,
        Nota_Comanda_Client_Dunca = 149,
        Comanda_Caraus_Aigle_Extracomunitar = 150,
        CMR = 151,
        Aviz_Expeditie_Lot_Marfa = 152,
        Aviz_Expeditie_Depozit_Lot_Marfa = 153,
        Aviz_Receptie_Depozit_Lot_Marfa = 154,
        ComandaContracte = 155,
        Documente_Camion = 156,
        Calatori_Programator_Intern = 157,
        Oferte_Delamode = 158,
        Aviz_Insotire_Marfa = 159,
        Raport_Managerial_ChCursa_Tabita = 160,
        Solduri_Pe_Persoane_Raport = 161,
        Fisa_Activitate_Cursa = 162,
        FacturaRoMM = 163, // FACTURA
        FacturaEngParteneriMM = 164, // INVOICE PARTENERI
        FacturaEngMultiposition = 165, // INVOICE MULTIPOSITION
        CMR_MM = 166,
        FacturaEngMM = 167, // INVOICE
        FacturaRoMultiposition = 168, // FALCTURS MULTIPOSITION
        AvizExpeditieDepozitMM = 169,
        ExpeditieRaportCamion = 170,
        FacturaRoCumulatMM = 171,
        FacturaEngCumulatMM = 172,
        CMR_Delamode = 173,
        Factura_Delamode = 174,
        Aviz_Expeditie_Lot_Marfa_Toate_MM = 175,
        Aviz_Expeditie_Depozit_Lot_Marfa_Toate_MM = 176,
        FoaieParcursViotrans = 177,
        FisaMagazieValorica = 178,
        Evidenta_Cursa = 179,
        FacturaAnexaMM = 180,
        FacturaInternMM = 181,
        ConfirmariSolduriClienti = 182,
        Comanda_Caraus_ContransportRO = 183,
        Comanda_Client_ContransportRo = 184,
        CMR_ContransportRO = 185,
        Licitatii_Produse_General = 186,
        Factura_Transmar = 187,
        Centralizator_Alimentari_Masini = 188,
        Centralizator_Alimentari_Tari = 189,
        Eticheta_ContransportRO = 190,
        Aviz_Expeditie_Marfa_Contransport = 191,
        FacturaContransportRO = 192,
        FacturaCumulatContransportRO = 193,
        AnexaFacturaContransportRO = 194,
        Comanda_Caraus_ContransportRO_FaraContract = 195,
        JurnalCumparariNou = 196,
        FacturaFiladelfiaCumulat = 196,
        AnexaFacturaFiladelfia = 197,
        AngajatiConcediiIntern_Tabita = 198,
        AngajatiConcediiExtern_Tabita = 199,
        Avizare_Livrare_ContransportRO = 200,
        JurnalVanzariNou = 201,
        JurnalCumparariNouA3 = 202,
        JurnalVanzariNouA3 = 203,
        Fisa_Partener = 204,
        Centralizare_Note_Contabile = 205,
        InvoiceFiladelfia = 206,
        Confirmare_Client_ContransportRO = 207,
        Factura_Delamode_Eng = 208,
        Factura_Delamode_Ferry = 209,
        Factura_Emisa_Eliton = 210,
        Factura_Delamode_OBI = 211,
        Factura_Delamode_Operational_RO = 212,
        Factura_Delamode_Operational_Eng = 213,
        Factura_Delamode_Operational_OBI = 214,
        Deviz_Estimativ_Dunca = 215,
        Factura_Emisa_Eliton_Exemplar2 = 216,
        Factura_Emisa_Eliton_Exemplar3 = 217,
        Deviz_Dunca_Parteneri = 218,
        Jurnal_Cumparari_Nou_TVA_Incasare = 219,
        Jurnal_Cumparari_Nou_TVA_Incasare_A3 = 220,
        Jurnal_Cumparari_A3_Viotrans = 221,
        Factura_Delamode_Eng_Obi = 222,
        Factura_Delamode_Eng_Operational_Obi = 223,
        Factura_Emisa_Cumulat_Eliton = 224,
        Factura_Emisa_Aigle_Valeo = 225,
        Factura_Emisa_Aigle_Faurecia = 226,
        Factura_Emisa_Cumulat_Exemplar2_Eliton = 227,
        Factura_Emisa_Cumulat_Exemplar3_Eliton = 228,
        Comanda_Caraus_Transmar = 229,
        Factura_Emisa_Rhenus = 230,
        Factura_Emisa_Maritim_Delamode = 231,
        Factura_Emisa_Cumulat_Delamode = 232,
        Comanda_Ferry_Calais = 233,
        Comanda_Ferry_DFDS = 234,
        Comanda_Caraus_Rehnus = 235,
        Jurnal_Cumparari_Nou_Cumulat = 236,
        Jurnal_Cumparari_Nou_Cumulat_A3 = 237,
        Jurnal_Cumparari_TVA_Incasare_Dunca = 238,
        Jurnal_Cumparari_TVA_Incasare_Dunca_A3 = 239,
        Factura_Emisa_Cursa_Delamode = 240,
        Factura_Emisa_Cursa_Eng_Delamode = 241,
        Factura_Emisa_Cumulat_Eng_Delamode = 242,
        Factura_Emisa_Maritim_Eng_Delamode = 243,
        Rentabilitate_Anvelope = 244,
        Factura_Emisa_Cumulat_Dunca = 245,
        Anexa_Factura_Emisa_Dunca = 246,
        Comanda_Caraus_Eliton = 247,
        Oferta_Contransport_Raport = 248,
        Confirmare_Transport_ContransportRO = 249,
        Jurnal_Cumparari_A4_Viotrans = 250,
        Factura_Anexa_Delamode = 251,
        Factura_Anexa_Eng_Delamode = 252,
        Jurnal_Vanzari_Nou_VioTrans = 253,
        Factura_Proforma_Delamode = 254,
        Factura_Proforma_Eng_Delamode = 255,
        AvizDeReceptie = 256,
        Comanda_Client_Viotrans_Service1 = 257,
        Raport_Managerial_Conta_Cheltuieli = 258,
        Solduri_Contracte_Leasing_Detaliat = 259,
        Solduri_Contracte_Leasing_Centralizat = 260,
        Deviz_Estimativ_Oferta_Viotrans = 261,
        Certificat_Control_Viotrans = 262,
        Verificari_Declaratia390 = 263,
        Verificari_Declaratia394 = 264,
        Verificari_Declaratia390_Achizitii = 265,
        Verificari_Declaratia394_Achizitii = 266,
        Factura_Emisa_Detaliat_Gerom = 267,
        Factura_Emisa_Cumulat_Gerom = 268,
        OrdinColectare_Delamode = 269,
        Factura_Emisa_Detaliat_CTP = 270,
        Factura_Emisa_Cumulat_CTP = 271,
        Factura_Anexa_Vama_Delamode = 272,
        Factura_Anexa_Vama_Eng_Delamode = 273,
        ChitantaValutaRaport = 274,
        Comanda_Caraus_Eng_Delamode = 275,
        Comanda_Caraus_CarTrans = 276,
        Comanda_Info_CarTrans = 277,
        NotificareFacturiScadente_Ro = 278,
        NotificareFacturiScadente_Eng = 279,
        Factura_Emisa_CarTrans = 280,
        Factura_Aerian_MM = 281,
        //Deviz_Estimativ_CTP = 282,
        CoverSheetAir_MM = 283,
        Factura_Aerian_Eng_MM = 284,
        Bon_Transfer_Detaliat = 285,
        Factura_Maritim_MM = 286,
        Factura_Maritim_Eng_MM = 287,
        Cover_Sheet_Maritim_MM = 288,
        Acceptare_Caraus_CarTrans = 289,
        FacturaMM_Storno_Aerian = 290,
        InvoiceMM_Storno_Aerian = 291,
        Factura_Emisa_Cumulat_Serviciu_Delamode = 292,
        Factura_Emisa_Cumulat_Serviciu_Eng_Delamode = 293,
        Factura_Emisa_MultiPage_CarTrans_Raport = 294,
        Evidenta_Incasari_Plati = 295,
        Factura_Emisa_Detaliat_ChimTotal = 296,
        LoadingUnloadingListWarehouse_Delamode = 297,
        AWB_Electronic = 298,
        Anexa_Factura_Emisa_Aigle = 299,
        FacturaEmisa_AlteFacturi_Contransport = 300,
        FacturaEmisaMultiposition_Contransport = 301,
        Invoice_Contransport = 302,
        ConfirmareTransport_Contransport = 303,
        Confirmare_Client_Contransport = 304,
        AvizExpeditie_Marfa_Contransport = 305,
        Eticheta_Contransport = 306,
        Avizare_Livrare_Contransport = 307,
        Comanda_Contracte_Contransport = 308,
        //Deviz_ChimTotal_Raport = 309,
        Factura_Emisa_Cumulat_CarTrans = 310,
        PV_Obiecte_Inventar = 311,
        CIM_Eliton = 312,
        ProofOfDelivery_Aerian_MM = 313,
        Avizare_Aerian_MM = 314,
        Preavizare_Aerian_MM = 315,
        Notificare_Eliton = 316,
        DecizieIncetareArt31 = 317,
        FacturaCumulataContransportDE = 318,
        SpeditionsauftragContransportD = 319,
        Comanda_Caraus_ContransportDE = 320,
        Comanda_Caraus_ContransportDE_FaraContract = 321,
        FacturaEmisaDetaliat_Marvi = 322,
        Comanda_Caraus_Marvi = 323,
        Comanda_Caraus_Eng_Marvi = 324,
        Comanda_Caraus_ContransportEng = 325,
        RaportAI_Contransport = 326,
        Factura_Proforma = 327,
        FacturaCumulataChimTotal = 328,
        Eticheta_ContransportEng = 329,
        AvizExpeditie_Marfa_ContransportEng = 330,
        Avizare_Livrare_ContransportEng = 331,
        Confirmare_Client_ContransportEng = 332,
        Fisa_Inventar_Gestiune = 333,
        FAZ_Neproductive = 334,
        Balanta_Materiale = 335,
        Produse_BarCode_ChimTotal_Raport = 336,
        //Deviz_Estimativ_ChimTotal = 337,
        DecizieIncetareArt65 = 338,
        DecizieIncetareArt55 = 339,
        DecizieIncetareArt56 = 340,
        DecizieIncetareArt81 = 341,
        InvoiceAlex = 342,
        ReferatCercetareDisciplinara = 343,
        MasterAWBElectronic_MM = 344,
        CargoManifestMM = 345,
        ReferatDepasireMotorina = 346,
        NotificareImputareTelefoane = 347,
        //NotificareFacturiScadente_I_Ro_Delamode = 348,
        //NotificareFacturiScadente_II_Ro_Delamode = 349,
        //NotificareFacturiScadente_I_Eng_Delamode = 350,
        //NotificareFacturiScadente_II_Eng_Delamode = 351,
        //SituatiiFacturi_Ro_Delamode = 352,
        //SituatiiFacturi_Eng_Delamode = 353,
        //NotificareFacturiScadente_Ro_Cartrans = 354,
        //NotificareFacturiScadente_Eng_Cartrans = 355,
        NotificareFacturiScadente_Ger = 348,
        Proces_Verbal_Constatare_Dunca = 356,
        ProofOfDeliverySea_MM = 357,
        Factura_Emisa_Anexa_Marvi = 358,
        FacturaAnexaMaritimDelamode = 359,
        FacturaEmisaCumulatMarvi = 360,
        BalantaAnaliticaCumulatSaseColoane = 361,
        BalantaAnaliticaNewSaseColoane = 362,
        BalantaSinteticaNewSaseColoane = 363,
        NotaContabila = 364,
        Solduri_Clienti_Furnizori_Cartrans = 365,
        Factura_Proforma_Cartrans = 366,
        Solduri_Clienti_Furnizori_Detaliat_Cartrans = 367,
        Fisa_Cont_Pt_Conditie_Cont_Valuta = 368,
        Fisa_Cont_Pt_Conditie_Cont_EchivalentLEI = 369,
        Comanda_Caraus_De_Transport_Cu_Subcontractori_Dunca = 370,
        Comanda_Caraus_De_Transport_Cu_Subcontractori_Engleza_Dunca = 371,
        Proces_Verbal_Predare_Primire_Dunca = 372,
        Confirmare_Client_Rhenus_RO = 373,
        Confirmare_Client_Rhenus_EN = 374,
        InventarGestiune = 375,
        FacturaEmisaDetaliatRozati = 376,
        FacturaEmisaCumulatRozati = 377,
        ComandaCarausRozatti = 378,
        Factura_Emisa_Cumulat_Rhenus = 379,
        Factura_Emisa_KSC = 380,
        Factura_Emisa_Cumulat_KSC = 381,
        Comanda_Caraus_KSC = 382,
        Factura_Anexa_Rhenus = 383,
        Factura_Anexa_KSC = 384,
        Comanda_Caraus_Rhenus_EN = 385,
        EvidentaDevizeDaune = 386,
        Inventar_MF = 387,
        FacturaEmisa_ITC = 388,
        FacturaEmisaCumulat_ITC = 389,
        Comanda_Caraus_Practicom = 390,
        Factura_Emisa_Practicom_MI = 391,
        PV_Receptie_Imobilizari = 392,
        Factura_Transporte_Popovici_Exemplar1 = 393,
        Factura_Transporte_Popovici_Exemplar2 = 394,
        Factura_Transporte_Popovici_Exemplar3 = 395,
        Factura_Transporte_Popovici_ExemplarCopie = 396,
        Factura_Transporte_Popovici_Exemplare = 397,
        Bon_Consum_Combustibil_View_Raport = 398,
        MF_PVLuareInPrimire = 399,
        MF_PVCasareOI = 400,
        MF_PVCasareMFCuRecuperare = 401,
        MF_PVCasareMFFaraRecuperare = 402,
        Alte_Doc_Gestiune_Raport = 403,
        Aviz_Insotire_Marfa_TP = 404,
        Foaie_de_Parcurs_Raport = 405,
        Jurnal_Vanzari_AplicaTvaIncasare = 406,
        ImpozitProfit = 407,
        ReferatRCAMalus = 408,
        ReferatAvertismentConsum = 409,
        ReferatDepasireViteza = 410,
        ReferatConstatareEvaluarePagube = 411,
        Statement_of_Account = 412,
        InventarCombustibilMasini = 413,
        Stoc_Combustibil_Masini = 414,
        Materiale_Gestiune = 415,
        Deviz_Service_Terti_Eur = 416,
        Deviz_Estimativ_Eur = 417,
        //Comanda_Caraus_Reporting = 418,
        Factura_Emisa_Detaliat_Logitrans_EN_Raport = 419,
        Factura_Emisa_Cumulat_Logitrans_EN_Raport = 420,
        Fisa_Partener_Pt_Conditie_Cont_EchivalentLEI = 421,

        DepozitIntrare = 422,
        DepozitIesire = 423,
        DepozitAvizInsotireMarfa = 424,
        AnexaFacturaDepozit = 425,

        RegistruCasaPerPagina = 426,
        Proces_Verbal_Fisa_Tehnica = 427,
        Program_De_Plata = 428,
    }

    public enum DataSourceType
    {
        Undefined,

        Nomenclator,

        Search,

        #region Export date
        ExportDate,
        ExportDateFormMain,
        #endregion

        #region Operational
        AsociereTelefoane,
        Transporturi,
        Comenzi_Predefinite,
        Curse_Constituire,
        Calatori_Curse_Constituire,
        Curse_Calcul_Profit,
        Curse_Evidente,
        Concedii,
        Finantari,
        Contract_Leasing,
        Asigurari,
        TransporturiGenerare_Wizard,
        Comenzi_Rutare_Zilnica,
        Curse_Rentabilitate,
        Cheltuieli_Parc_Auto,
        Cheltuieli_Cursa,
        Voyage_Report,
        Foaie_Parcurs_Consum_Tronsoane,
        Foaie_Parcurs_Consum_Tronsoane_Split,
        Foaie_Parcurs_X_Split,
        Foaie_Parcurs,
        FAZ_Neproductive,
        Curse_Evenimente,
        Partide_Paleti,
        ImportCombusitibl_Card,
        ImportCombustibil_Pompa,
        ImportCombustibilCard_Decont,
        ManagementImporturiCombustibilPompa,
        Foaie_Parcurs_Comenzi,
        Partide_Tarifare,
        Carnete_TIR,
        Autorizatii,
        Livrari_Comenzi,
        ImportEDI,
        ExportEDI,
        Transporturi_CrossDocking,
        FoaieDeParcursGps,
        PartideValidareFacturare,
        ImportLinde,
        PG_Comenzi,
        FoaieParcursImportGps,
        Foaie_Parcurs_Consum_Tronsoane_Viotrans,
        Alimentari_Tancuri,
        ImportComenziFerry,
        TransporturiPredareDocumente,
        ImportAnexaAsigurari,
        Rentabilitate_Anvelope,
        ImportFacturiEliton,
        GrilaTarifeMarvi,
        Ruta,
        AngajatiDiagrame,
        ImportTelefoane,
        VizualizareTelefoane,
        VizualizareComenziInLucru,
        ImportDate_Vizualizare,
        Vizualizare_Comenzi_Toate,
        Vizualizare_Curse_Cumulat,
        Profitabilitate_Curse,
        Control_Financiar_Cursa,
        FoaieParcursAlimentariCuNumerar,
        Vizualizare_Transporturi_Nefacturate,
        Vizualizare_Comenzi_Nevalidate_Pentru_Facturare,
        Verificare_Facturare,
        Vizualizare_Comenzi_Predefinite,
        CursePreluareTronsoaneGPS,
        TranzactiiAlimentariCombustibil,
        CursePreavizareDepozit,
        PartideEditareCantitati,
        AlocareVenitComenziSplitate,
        TrimitereEmailCarausi,
        PanouriScadente,
        ManagementImporturiExcel,
        DiurnaPredefinita,
        DiurneSoferiCalcul,
        CurseAsociate,
        SplitarePartida,
        VizualizareRapoarte,
        Import_Conenzi_GPS,
        PartideEditarePU,
        #endregion Operational

        #region Calatori

        Calatori_Foi_Parcurs,
        Calatori_Transporturi,
        Calatori_Programare_Soferi,
        Calatori_Pontaj_Soferi,
        Calatori_Import_Devize,
        Calatori_EBIDTA,
        Calatori_Import_Pontaj_Extern,
        Calatori_Planificator,
        SelectareSoferi,
        #endregion Calatori

        #region HR

        ImportSalarii,
        Angajati_Adeverinta,
        CIM,
        Notificare,
        Decizie_Incetare_CIM,
        Referat_Cercetare_Disciplinara,
        AngajatiPontaj,
        CentralizatorPontaj,
        GenerarePlatiSalarii,
        ReferatHR,
        Vizualizare_Foi_Concediu,
        Angajati_ActAditional,
        Angajati_Notificare_Incetare,
        Angajati_Decizie_Incetare,
        Angajati_CIM_Vizualizare,
        Angajati_DecizieColectivaSalariala,
        Angajati_DecizieColectivaSalariala_Vizualizare,
        Angajati_NotaLichidare,
        Angajati_CerereConcediu,
        Angajati_CerereConcediu_Vizualizare,
        ReferatImputareAmenda,
        ReferatDisciplinar,
        #endregion HR

        #region Juridic

        Dosar,
        Vizualizare_Facturi_Emise_Juridic,
        Insolventa,

        #endregion Juridic

        #region Facturi

        FacturaEmisa,
        FacturaPrimita,
        Factura_Emisa_Storno_Wizard,
        Factura_Primita_Storno_Wizard,
        Stingere_Facturi_Sosite,
        Stingere_Facturi_Nefacturate,
        FacturaProforma,
        BonFiscal,
        Vizualizare_Facturi_Emise,
        Vizualizare_Facturi_Primite,
        CreditNote,
        Nomenclator_Sabloane_Facturi,
        VizProduseActualizate,
        VizualizareFEeFactura,

        #endregion Facturi

        #region Facturare

        Partide_Facturare,
        Curse_Facturare,
        CurseExpeditii_Facturare,
        GenerareFacturiTransporturi,
        Devize_Facturare,
        GenerareFacturiCombustibilCard,
        AnexaCombustibilCardFacturi,
        GenerareFacturiDepozit,
        GenerareFacturiContracte,
        Refacturare,
        AvizeFacturare,
        FacturaProformaPopup,
        #endregion Facturare

        #region Financiar

        Chitanta,
        AlteOpJurnaleTVA,
        Banca,
        Casa,
        Decont_Terti,
        CEC,
        Decont_Cursa_General,
        Dispozitii_Plata_Incasare,
        OP,
        Facturi_Neincasate,
        Facturi_Neplatite,
        Curs_Valutar,
        Curs_Valutar_Online,
        Decont_Cursa_Avansuri_Resturi,
        Decont_Cursa_Avansuri_Resturi_Complex,
        Inchidere_Avansuri,
        PV_Compensare,
        PV_Compensare_Actualizare_Facturi,
        Decont_Cursa_Diurna,
        DCG_Dunca,
        Transfer_Avansuri,
        DCG_Cheltuieli_Numerar,
        Foaie_Parcurs_Alte_Consumuri,
        Cash_Flow_Previzionat,
        EvidentaDevizeDaune,
        Alimentare_Combustibil_Subcontractanti,
        FacturiNeplatiteExport,
        FacturiNeplatiteExportGrupate,
        Cheltuieli_Diverse,
        Observatii_Factura,
        Vizualizare_Observatii_Factura,
        Compensari_Deconturi,
        #endregion Financiar

        #region Firme

        Firma_Proprie,
        Firme_Detalii,
        Fisa_Angajat,
        Angajati_Concedii,
        Foi_Concediu_Active,
        Fisa_Tehnica_Masini_Proprii,
        Fisa_Inventar,
        Daune,
        Fisa_Tehnica_Aeronave,
        Firme_Adrese_De_Livrare,
        Evidenta_Carduri,
        Fisa_Tehnica_Masini_Terti,
        Masini_Carausi,
        Angajati_Cazier,
        Fisa_Alimentare_Motorina,
        CentralizatorCazier,
        Masini_Indisponibile,
        Formular_Achizitii,
        FirmeContracte,
        AngajatiMasiniAsociere,
        ExportFirmeSiFacturiEmise,
        VizualizareEvenimenteAngajati,
        VizualizareProgramariService,
        Firme_Contracte_Detalii,
        #endregion Firme

        #region Gestiune

        Lista_Produse,
        NIR,
        Aviz_Insotire_Marfa,
        Bon_De_Consum,
        Bon_Consum_Serii_Produse,
        Actualizare_Produse_Stoc,
        Bon_Transfer,
        Alte_Documente_Gestiune,
        Stoc_Zilnic,
        Aviz_Generare_NIR,
        NIR_Facturi_Sosite,
        NIR_Generare_Bon_Consum,
        Stoc_Initial_Persoane,
        PV_Transfer_Gestiune_Persoane,
        Actualizare_Stoc_Persoane,
        Pompa_Generare_Bon_Consum,
        Fisa_Magazie,
        FisaDeMagazieVizualizare,
        Stoc_Initial_Inventar,
        InventarGestiune,
        Stoc_Combustibil_Masini,
        Vizualizare_Stoc_Combustibil_Masini,
        Bon_Transfer_Combustibil,
        Bon_Consum_Combustibil,
        Masini_Proprii_Rest_Rezervor_Lunar,
        Actualizare_Produse_Stoc_Combustibil,
        GestiuneStornoNir,
        InventarCombustibilMasini,
        StocPeIntervaleVizualizare,
        GenerareBonTransferCombustibilPompa,
        GenerareBonConsumCombustibilStorno,
        Retetar,
        RaportIstoricPretAchizitie,
        #endregion Gestiune

        #region Repartizare

        RepartizareVenituri,
        RepartizareCosturi,
        RepartizareCosturi_BonConsum,
        ActualizareFacturi,
        ActualizareCEC,
        ActualizareDaune,
        ActualizareAsigurari,
        FacturiRecuperareTVA,
        ActualizareDeconturi,
        EditareRepartizareCosturi,
        ActualizareFinantari,
        RepartizareCosturiSuplimentareMarfa,
        RepartizareVenituriNew,
        #endregion Repartizare

        #region Registratura

        Registratura,
        Registratura_FE,
        Registratura_Expeditii,
        TruckTraceExpeditii,
        PrintarePlicuri,
        #endregion Registratura

        #region Service

        Schimburi,
        Comanda_Client_Service,
        Deviz,
        Centralizator_Devize,
        Centralizator_Operatii_Mecanici,
        Deviz_First_Truck,
        Deviz_Estimativ,
        Licitatii_Produse_Service,
        Programari_Utilaje_Service,
        Programari_Mecanici_Service,
        Nota_Comanda_Client,
        Nota_Comanda_Produse,
        Vizualizare_Note_Comenzi_Atasate,
        Licitatii_Produse,
        Lista_Operatii_Manopera,
        Necesar_Comanda_Client,
        Licitatii_Produse_Vizualizare,
        Pontaj_Mecanici_Timp_Real,
        Proces_Verbal_Constatare,
        Operatii_mecanici,
        Proces_Verbal_Predare_Primire,
        ProgramariService,
        RaportEveniment,
        #endregion Service

        #region Contabilitate

        #region Contabilitate vizualizari

        FacturiActualizareViewer,
        NoteContabileViewer,
        ReevaluareViewer,
        CosturiRepartizateViewer,
        CECActualizareViewer,
        FirmeCIFInvalid,
        Balanta_Mijloace_Fixe,
        JurnalCumparariNou,
        JurnalVanzariNou,
        SoldulLaData,
        NotificareFacturiScadente,
        FisaContNoua,
        Vizualizare_Note_Contabile_Toate,
        RegistrulBunurilorDeCapital,
        VizualizareFacturiReevaluare,
        MiscariMijloaceFixe,
        Verificare_Balanta,
        RenumerotareDocumente,
        CarteaMare,
        VizualizareInventarMF,
        #endregion Contabilitate vizualizari

        #region Contabilitate Operatii

        NoteContabile,
        Mijloace_Fixe,
        Reevaluare,
        Compensare,
        Registru_Jurnal,
        Balanta,
        NoteContabileSalarii,
        Declaratii,
        FisaPePersoana,
        SolduriClientiFurnizori,
        ConfirmariSolduriClientiFurnizori,
        BlocareDocumente,
        CentralizatorConturi,
        CentralizatorCasaBanca,
        FisaContului,
        FacturiPrimtieNeexigibile,
        ConfirmariSolduriClienti,
        CentralizatorBalanteSolduri,
        FirmeIncasareTVA,
        FisaPartener,
        Centralizare_Note_Contabile,
        ReevaluareCasaBanca,
        EstimariCosturiCurse,
        ReevaluareSolduriPersoane,
        ReevaluareSolduriConturiValuta,
        RepartizareFiliale,
        RepartizareFilialeDetalii,
        RepartizareCentreProfit,
        RepartizareCentreProfitDetalii,
        VizualizareNoteContabileSalarii,
        Provizioane,
        Casare,
        FirmeDeclaratia394,
        Egalizare_Solduri_Clienti_Furnizori,
        Inventar_MF,
        BilantFormulare,
        BilantVerificare,
        MF_ProcesVerbal_Generare,
        MF_ProcesVerbal,
        AlteRepartizariCosturi,
        ReevaluareMF,
        FacturiStornareAvans,
        ConfigurarePL,
        ConfigurariContabilitate,
        Factoring,
        ImpozitProfit,
        #endregion Contabilitate Operatii

        #endregion Contabilitate

        #region Administrare

        Utilizator,

        GrupUtilizatori,

        ChangePassword,

        ViewLogs,

        MergeTables,

        ChangePasswordNew,

        UtilizatorNew,

        #endregion Administrare

        #region Vizualizari

        FilteredView,
        MasterChildFilteredView,

        #endregion Vizualizari

        #region Scadente

        ScadenteMasiniViewer,
        ScadenteAngajatiViewer,
        ScadenteColors,
        ScadentePeriodColors,
        ScadenteSolicitariCotatii,
        ScadenteRegistraturaExpeditii,

        #endregion Scadente

        #region Atasamente

        Atasamente,
        Vizualizare_Atasamente,
        #endregion Atasamente

        #region Templates

        Templates,

        #endregion Templates

        #region Management

        Ponderi_Cheltuieli_Firma,
        Ponderi_Venituri_Firma,
        CashFlow_Pivot,
        Cifra_Afaceri,
        Profit_Loss,
        SoldGeneral_Rentabilitate_Firma,
        Ponderi_Clienti,
        Soferi_Beneficii,
        Disponibilitati_CasaBanca,
        Grafic_Activitate,
        Evidenta_Facturare,
        Sold_Persoane,
        Scadente_Facturi,
        EBIDTA_Camion,
        Profit_Loss_Expeditii,
        Profit_Loss_Expeditii_Detaliat,
        Evidenta_Incasari_Plati_MM,
        //Forecast,
        //ForecastVizualizare,
        //StatisticiDTD,
        ManagerRaportEBIDTA,
        //CostPerPaletPerKg,
        //ContabilitateVsRaportOperational,
        Profit_Loss_Delamode,
        //EvolutieGrupaje,
        RaportPeReferinta,
        Controlling,
        Management_Consumuri,
        #endregion Management

        #region Nomenclator

        NomenclatorViewer,

        #endregion Nomenclator

        #region Comercial

        Comercial_Tarifare,
        Comercial_Oferte,
        Vizualizare_Activitate_Vanzari,

        #endregion Comercial

        #region Expeditii

        LotMarfa,
        Grupaje,
        GrupajeXCurse,
        LotMarfaXGrupaje,
        LotMarfaXCurse,
        CamionConstituire,
        House,
        Master,
        LotMarfaSchimbareLinie,
        Operatiuni_Vamale,
        Warehouse,
        WarehouseDelamode,
        EDI_Loader,
        EDI_GeneratorFirme,
        CamionConstituireDelamode,
        Solicitari,
        Oferte,
        CMR,
        DocumenteExpeditie_Marfa,
        LotMarfaTarifare,
        LotMarfaReguliTarifare,
        LotMarfaCalculTarifare,
        CamionGenerareCMR,
        CamionGenerareDocumente,
        Contracte,
        CamionDocumente,
        LotMarfaTarifar,
        Comanda_Caraus,
        CamionConstituireFTLProject,
        LoturiCurseFacturiEmisePrimite,
        LotMarfaPreavizariDelamode,
        ExpeditiiFacturaPrimitaBunPlata,
        ComandaFerry,
        ImportTranzactiiDKV,
        Bookings,
        WarehouseFinanciar,
        KaercherEDI,
        RaportOperationalExpeditii,
        RaportOperationalExpeditiiDomestic,
        RaportOperationalExpeditiiFTLProject,
        StatusReports,
        VizualizareScadenteOperational,
        Vizualizare_Camioane_Fara_DataIncarcare,
        Confirmare_Comanda,
        Firme_Credit_Control,
        OrdinColectare,
        Vizualizare_Validat_Facturare,
        RaportOperationalAerianMaritim,
        LotMarfaVizualizare,
        WarehouseTreeViewDelamode,
        #endregion Expeditii

        #region Finantari

        Solduri_Contracte_Leasing,
        ReevaluareFinantari,
        ValidareFacturare,

        #endregion Finantari

        #region Management Anvelope

        ManagementAnvelope,

        #endregion

        Vizualizare_Diurna_Sofer,
        VizualizareImportCardFacturi,
        RapoarteCustom,
        ControlSelectieLookup,
        AdministrareControlSelectieLookup,
        NomenclatorCulori,

        #region TransporturiTrust (doar pentru Dolotrans)
        ImportVehiculeTrust,
        CreareCursaImportTrust,
        ReceptionareInDepozitTrust,
        CreareCursaDistibutieTrust,
        CalculTarifStocTrust,
        VizualizareVehiculeTrust,
        #endregion TransporturiTrust (doar pentru Dolotrans)

        #region Dolotrans
        ImportCumparareVanzareVehicule_Wizard,
        #endregion Dolotrans

        VizualizariProcedura,

        #region Planificator
        Planificator_Verificari,
        Vizualizare_Partide_Neplanificate,
        Clasificare_Planificare,
        Nomenclator_Clasificari_Asignare_Coloane_Planificator,
        Curse_Actiuni,
        StatusVehicule,
        PlanificatorMesagerie,
        AsociereResurse,
        ResurseCreazaCursa,
        SelectieSemiremorca,
        SchimbareResurse,
        Mesagerie,
        PlanificatorAsocierePartidaParinte,
        MesagerieVizualizare,
        ActiuniCuse_AdaugaPauza,
        #endregion

        #region Warehouse
        Actualizare_Produse_Stoc_Depozit,
        Vizualizare_Stoc_Depozit,
        #endregion

        #region PlanificatorNew
        Planificator_New,
        PlanificareVizualizare,
        PlanificatorStatusuriComenzi,
        PlanificareSoferiNew,
        #endregion

    }

    public enum UserInterfaceType : int
    {
        Undefined = 0,

        #region Facturi emise

        Factura_Emisa_Operational = 1,
        Factura_Emisa_Altele = 2,
        Factura_Emisa_Storno = 4,
        Factura_Emisa_SoldInitial = 5,
        Factura_Emisa_Storno_Wizard = 8,
        Factura_Emisa_Comert = 9,
        Factura_Emisa_Service = 10,
        Factura_Emisa_Operational_Expeditii = 15,
        Generator_Facturi_Expeditii = 16,
        Generator_Facturi_Vamale_Expeditii = 18,
        BonFiscal = 31,
        Factura_Emisa_Avize = 19,

        #endregion Facturi emise

        #region Facturi primite

        Factura_Primita_Caraus = 3,
        Factura_Primita_Altele = 6,
        Factura_Primita_Sold_Initial = 7,
        Factura_Primita_Storno = 11,
        Factura_Primita_Storno_Wizard = 12,
        Factura_Primita_Achizitie = 13,
        Factura_Primita_Sosita = 14,
        Factura_Primita_Caraus_Expeditii = 17,

        #endregion Facturi primite

        #region Facturi Proforme

        Factura_Proforma = 30,

        #endregion Facturi Proforme

        #region Financiar

        Chitanta = 400,
        Casa = 401,
        Banca = 402,
        Dispozitii_PI = 403,
        AlteOpJurnaleTVA = 404,
        Decont_Terti = 405,
        CEC = 406,
        Decont_Cursa_General = 407,
        Dispozitii_Plata_Incasare = 408,
        OP = 409,
        Facturi_Neincasate = 410,
        Facturi_Neplatite = 411,
        Curs_Valutar = 412,
        Inchidere_Avansuri = 413,
        PV_Compensare = 414,
        Decont_Terti_Grid = 415,
        DCG_Dunca = 416,
        Transfer_Avansuri = 417,
        DCG_Cheltuieli_Numerar = 418,
        Inchidere_Decont = 419,
        Cash_Flow_Previzionat = 420,

        #endregion Financiar

        #region Firme

        Firma_Proprie = 500,
        Firme_Detalii = 501,
        Fisa_Angajat = 502,
        Angajati_Concedii = 505,

        Masini_Terti = 503,
        Fisa_Tehnica_Masini_Proprii = 504,
        Daune = 506,
        Fisa_Invetar = 507,
        Fisa_Tehnica_Aeronave = 508,
        Firme_Adrese_De_Livrare = 509,
        Evidenta_Carduri = 510,
        Foi_Concediu_Active = 511,
        Fisa_Tehnica_Masini_Terti = 512,
        Masini_Carausi = 513,
        Angajati_Cazier = 514,
        Fisa_Alimentare_Motorina = 515,
        Masini_Disponibile = 516,
        Firme_Detalii_Cartrans = 517,
        Formular_Achizitii = 518,
        FirmeContracte = 519,
        #endregion Firme

        #region Operational-transporturi

        #region Transporturi

        Comenzi_Predefinite = 601,
        Transporturi = 602,
        TransporturiGenerare_Wizard = 604,
        Voyage_Report = 610,
        Partide_Tarifare = 616,
        Livrari_Comenzi = 617,
        //Rentabilitate_Anvelope = 618,

        #endregion Transporturi

        #region Curse

        Curse_Constituire = 603,
        Curse_Rentabilitate = 609,
        Curse_Evenimente = 613,
        Curse_Evidente = 620,
        Calatori_Curse_Constituire = 621,

        #endregion Curse

        #region Cheltuieli flota

        Asigurari = 605,
        Finantari = 606,
        Cheltuieli_Parc_Auto = 607,
        Cheltuieli_Cursa = 608,
        FAZ_Neproductive = 615,
        Foaie_Parcurs_Consum_Tronsoane = 611,
        Foaie_Parcurs = 612,
        Foaie_Parcurs_Consum_Gps = 618,
        Foaie_Parcurs_Import_Gps = 619,
        Foaie_Parcurs_Consum_Tronsoane_Split = 622,
        Foaie_Parcurs_Consum_Tronsoane_Gps = 623,
        Ruta = 625,

        #endregion Cheltuieli flota

        #region Rutare zilnica

        Comenzi_Rutare_Zilnica = 614,

        #endregion Rutare zilnica

        GrilaTarifeMarvi = 624,

        #endregion Operational-transporturi

        #region HR

        Angajati_Adeverinta = 200,
        CIM = 201,
        Notificare = 202,
        DecizieIncetareArt31 = 203,
        DecizieIncetareArt65 = 204,
        Notificare_Decizie_Imputare_Telefoane = 205,
        DecizieIncetareArt55 = 206,
        DecizieIncetareArt56 = 207,
        DecizieIncetareArt81 = 208,
        Referat_Cercetare_Disciplinara = 209,
        Referat_Depasire_Motorina = 210,
        ReferatRCAMalus = 211,
        ReferatAvertismentConsum = 212,
        ReferatDepasireViteza = 213,
        ReferatConstatareEvaluarePagube = 214,
        Angajati_ActAditional = 215,
        Angajati_Notificare_Incetare = 216,
        Angajati_Decizie_Incetare = 217,
        Angajati_DecizieColectivaSalariala = 218,
        Angajati_NotaLichidare = 219,
        Angajati_CerereConcediu = 220,
        ReferatImputareAmenda = 221,
        ReferatDisciplinar = 222,
        #endregion HR

        #region Juridic

        SomatiiDePlata = 250,
        Insolventa = 251,
        PlangeriContraventionale = 252,
        PlangeriPenale = 253,
        LitigiiDeMunca = 254,
        ProceseAvocati = 255,
        DeciziiDeConcediere = 256,
        AngejamenteDePlata = 257,
        ExecutariSilite = 258,
        LitigiiCuAsiguratorii = 259,
        VizualizareFacturiEmiseClient = 260,

        #endregion Juridic

        #region Registratura

        Registratura = 700,
        Registratura_FE = 701,
        Registratura_Expeditii = 702,
        TruckTraceExpeditii = 703,
        VizualizareRegistraturaFacturiPrimite = 704,
        PrintarePlicuri = 705,
        ListareConfirmare_ClientRoman = 706,
        ListareConfirmare_ClientStrain = 707,
        ListareBorderou = 708,
        //Comanda_Caraus_Rep = 708,

        #endregion Registratura

        #region Gestiune

        Lista_Produse = 750,
        NIR = 751,
        Aviz_Insotire_Marfa = 752,
        Bon_De_Consum = 753,
        Bon_Transfer = 754,
        Alte_Documente_Gestiune = 755,
        Stoc_Initial = 756,
        Aviz_Insotire_Marfa_Iesire = 757,
        Alte_Documente_Gestiune_Intrare = 758,
        PV_Transfer_Gestiune_Persoane = 759,
        Fisa_Magazie = 760,
        Balanta_Materiale = 761,
        InventarGestiune = 762,
        UrmarireProduse = 763,
        UrmarireCombustibilMasini = 764,
        Bon_Transfer_Combustibil = 765,
        Vizualizare_Taxe_De_Drum = 766,
        InventarCombustibilMasini = 767,
        Materiale_Gestiune = 768,
        Bon_Consum_Combustibil = 769,
        
        #endregion Gestiune

        #region Service

        Schimburi = 800,
        Comanda_Client_Service = 801,
        Deviz = 802,
        Deviz_Estimativ = 803,
        Licitatii_Produse = 804,
        Management_ProgramariUtilaje = 805,
        Management_ProgramariMecanici = 806,
        Nota_Comanda_Client = 807,
        Licitatii_Produse_Dunca = 808,
        Lista_Operatii_Manopera = 809,
        Licitatii_Produse_Form_Vizualizare = 810,
        RaportEveniment = 811,
        //Deviz_Estimativ_VioTrans = 811,
        Proces_Verbal_Constatare = 812,
        Proces_Verbal_Predare_Primire = 813,
        Vizualizare_RaportEveniment = 814,
        #endregion Service

        #region CarneteTIR_Autorizatii

        Carnete_TIR = 300,
        Autorizatii = 301,

        #endregion CarneteTIR_Autorizatii

        #region Administrare

        Utilizator = 9000,
        GrupUtilizatori = 9001,
        SQLManager = 9002,
        Entitati = 9003,
        VizualizariProcedura = 9004,
        UtilizatorNew = 9005,

        #endregion Administrare

        #region Comercial

        Comercial_Tarifare = 950,
        Comercial_Oferte = 951,

        #endregion Comercial

        #region Vizualizari

        Registratura_Vizualizare = 2000,
        Registratura_Vizualizare_MC = 2001,
        Partide_Facturate_Factura_Vizualizare = 2002,
        Repartizare_Venituri_Factura_Vizualizare = 2003,
        Registratura_Vizualizare_Facturi_Emise_Neexpediate = 2004,
        Registratura_Vizualizare_Facturi_Primite_Neinregistrate = 2005,
        Facturi_Emise_Vizualizare = 2006,
        Facturi_Primite_Vizualizare_Repartizare_Cheltuieli = 2007,
        Facturi_Emise_Vizualizare_Detalii = 2008,
        Facturi_Emise_Vizualizare_Venituri_Repartizate = 2009,
        Curse_Vizualizare = 2010,

        [VisualisationMenuItem("barSubItem2", "mnuExportXml", "Export XML", "UI.Firme.FisaTehnicaExportToOrtec", "ExportToOrtec")]
        Masini_Vizualizare = 2011,

        Masini_Vizualizare_Scadente = 2012,
        Daune_Vizualizare = 2013,

        [VisualisationMenuItem("barSubItem2", "mnuExportXml", "Export XML", "UI.Firme.FirmeDetaliiContact_ExportToOrtec", "ExportToOrtec")]
        Firme_Vizualizare = 2014,

        [VisualisationMenuItem("barSubItem2", "mnuExportXml", "Export XML", "UI.Firme.FisaAngajati_ExportToOrtec", "ExportToOrtec")]
        Angajati_Vizualizare = 2015,
        Curse_Vizualizare_Evenimente = 2016,
        Cheltuieli_Cursa_Vizualizare = 2017,
        Foi_De_Concediu = 2018,
        Cheltuieli_Parc_Auto_Vizualizare = 2019,
        Finantari_Vizualizare = 2020,
        Asigurari_Vizualizare = 2021,
        Balanta_Clienti_Furnizori = 2022,
        Fisa_Furnizor = 2023,
        Facturi_Primite_Vizualizare = 2024,
        Casa_Vizualizare = 2025,
        Banca_Vizualizare = 2026,
        Curse_Vizualizare_Cumulat = 2027,
        DispozitiiPI_Vizualizare = 2028,
        Comenzi_In_Lucru = 2029,
        Control_Financiar_Curse = 2030,
        Comenzi_Predefinite_Vizualizare = 2031,
        Decont_Alte_Cheltuieli_Vizualizare = 2032,
        NIR_Vizualizare = 2033,
        Transporturi_Nefacturate = 2034,
        Profitabilitate_Curse = 2035,
        BC_Vizualizare = 2036,
        BT_Vizualizare = 2037,
        ADGestiune_Vizualizare = 2038,
        CEC_Vizualizare = 2039,
        Inventar_Vizualizare = 2040,
        Decont_Terti_Vizualizare = 2041,
        Voyage_Report_Vizualizare = 2042,
        Validare_Pentru_Facturare_Vizualizare = 2043,

        [VisualisationMenuItem("barSubItem2", "mnuExportXml", "Export XML", "UI.Firme.FirmeAdreseLivrare_ExportToOrtec", "ExportToOrtec")]
        Vizualizare_Adrese_Incarcare_Descarcare = 2044,

        Schimburi_Vizualizare = 2045,
        Decont_Curse_Vizualizare = 2046,
        Decont_Foi_Parcurs_Vizualizare = 2047,
        Foi_Parcurs_Cheltuieli_Cursa = 2048,
        Foi_Parcurs_Vizualizare = 2049,
        Evidenta_Carduri_Vizualizare = 2050,
        Fisa_Client = 2051,
        Comanda_Client_Service_Vizualizare = 2052,
        Import_Combustibil_Card = 2053,
        Import_Combustibil_Pompa = 2054,
        Vizualizare_Produse_Gestiune = 2055,
        Vizualizare_Stoc_Marfa = 2056,
        Vizualizare_Stoc_Initial = 2057,
        Jurnal_Vanzari = 2058,
        Jurnal_Cumparari = 2059,
        Contabilitate_Vizualizare_Note_Contabile_Toate = 2060,
        Deviz_Service_Vizualizare = 2061,
        Stoc_Critic = 2062,
        Stoc_Maxim = 2063,
        Factura_Primita_Achizitie_Vizualizare = 2064,
        Vizualizare_Intrari_Iesiri = 2065,
        Vizualizare_Facturi_Comert = 2066,
        Fisa_De_Magazie = 2067,
        Vizualizare_Note_Reevaluare = 2068,
        Vizualizare_Facturi_Reevaluare = 2069,
        Centralizator_Facturi = 2070,
        Comanda_Client_Service_Daune_Vizualizare = 2071,
        Facturi_Emise_Service_Vizualizare = 2072,
        Masini_Terti_Service_Vizualizare = 2073,
        Procese_Verbale_De_Compensare = 2074,
        Salarii_Vizualizare = 2075,
        Vizualizare_Note_Salarii = 2076,
        Solduri_Initiale_Persoane_Vizualizare = 2077,
        Vizualizare_Mijloace_Fixe = 2078,
        Scadente_Asigurari = 2079,
        Scadente_Finantari = 2080,
        Verificari_Facturi_Emise = 2081,
        Verificari_Facturi_Primite = 2082,
        Verificari_Banca = 2083,
        Verificari_Casa = 2084,
        Verificari_Cheltuieli_Cursa = 2085,
        Verificari_Cheltuieli_ParcAuto = 2086,
        Verificari_Cheltuieli_Terti = 2087,
        Verificari_Declaratia390 = 2088,
        Verificari_Declaratia394 = 2089,
        Vizualizare_Autofacturare = 2090,
        Vizualizare_Facturi_Emise_Incasate = 2091,
        Vizualizare_Facturi_Primite_Platite = 2092,
        Gestiune_Verificare = 2093,
        Gestiune_Verificare_Detaliat = 2094,
        Vizualizare_Import_Combustibil_Anexe_Facturi = 2095,
        Centralizator_Recuperare_TVA = 2096,
        Vizualizare_Carnete_TIR = 2097,
        Avize_Insotire_Marfa_Vizualizare = 2098,
        Vizualizare_Autorizatii = 2099,
        Comenzi_Vizualizare_Toate = 2100,
        Cazier_Vizualizare = 2101,
        Livrari_Comenzi_Vizualizare = 2102,
        ControlCurseProprii = 2103,
        ControlCurseIntermediate = 2104,
        ControlCurseToate = 2105,
        VizualizareFinanciara = 2106,
        VizualizareFinanciaraCurseProprii = 2107,
        VizualizareAvansuriResturiNedecontate = 2108,
        CautareMasiniCarausi = 2109,
        ChitantaVizualizare = 2110,
        Foi_De_Concediu_Toate = 2111,
        Vizualizare_Tranzite = 2112,
        Vizualizare_Masini_Carausi = 2113,
        Vizualizare_Firme_Rute_Negociate = 2114,
        Deviz_Estimativ_Vizualizare = 2115,
        Licitatii_Produse_Vizualizare = 2116,
        Manager_Vizualizare_Reparatii = 2117,
        Manager_Vizualizare_Km_Parcursi = 2118,
        Manager_Rentabilitate_Curse = 2119,
        Manager_Rentabilitate_Dispecer = 2120,
        Manager_Diferente_Rest_Rezervor = 2121,
        Facturi_Sosiste_Vizualizare = 2122,
        PV_Transfer_Gestiune_Persoane_Vizualizare = 2123,
        Fisa_De_Magazie_Pe_Persoana = 2124,
        Piese_De_Comandat_Gestiune = 2125,
        Piese_Comandate_Gestiune = 2126,
        Urmarire_Mecanici = 2127,
        Bon_Consum_Vizualizare_Print = 2128,
        Cheltuieli_Flota_Vizualizare = 2129,
        Vizualizare_Transfer_Avansuri = 2130,
        Vizualizare_Decont_Cursa_Financiar = 2131,
        Centralizator_Anvelope = 2132,
        LotMarfa_Cumulat_Vizualizare = 2133,
        Fisa_Alimentare_Motorina_Vizualizare = 2134,
        LotMarfa_Aerian_Vizualizare = 2135,
        LotMarfa_Maritim_Vizualizare = 2136,
        LotMarfa_Aerian_Toate_Vizualizare = 2137,
        LotMarfa_Maritim_Toate_Vizualizare = 2138,
        Vizualizare_LotMarfa_Marfuri_Toate = 2139,
        Vizualizare_Operatiuni_Vamale_Toate = 2140,
        Vizualizare_LotMarfa_Marfuri = 2141,
        Vizualizare_LotMarfa_Marfuri_Detalii = 2142,
        Vizualizare_Curse_Toate = 2143,
        Vizualizare_Curse_Grupaj = 2144,
        Vizualizare_Curse_Detalii = 2145,
        Vizualizare_Curse_Grupaj_Detalii = 2146,
        Vizualizare_Curse = 2147,
        Vizualizare_House_Aerian = 2148,
        Vizualizare_Master_Aerian = 2149,
        Vizualizare_House_Aerian_TipLotMarfa = 2151,
        Vizualizare_Master_Aerian_TipLotMarfa = 2152,
        Vizualizare_Operatiuni_Vamale_In_Lucru = 2153,
        Vizualizare_Operatiuni_Vamale_Finalizate = 2154,
        Vizualizare_Operatiuni_Vamale_Anulate = 2155,
        Vizualizare_Warehouse_Toate = 2156,
        Vizualizare_Warehouse_Receptionate = 2157,
        Vizualizare_Warehouse_Expediate = 2158,
        Vizualizare_Warehouse_Anulate = 2159,
        Comenzi_Livrari_VioTrans = 2160,
        Vizualizare_Masine_Proprii_Detalii_Aerian = 2161,
        Vizualizare_Juridic = 2162,
        Programare_Soferi_Decont_Vizualizare = 2163,
        Vizualizare_Foi_Parcurs_FilteredView = 2164,
        Calatori_FoiParcurs_Vizualizare = 2165,
        Loguri_Vizualizare = 2166,
        Vizualizare_Comercial_Oferte = 2167,
        Vizualizare_Comercial_Solicitari = 2168,
        Vizualizare_Warehouse_Toate_Intrare_MM = 2169,
        Vizualizare_Warehouse_Toate_Iesire_MM = 2170,
        Vizualizare_Warehouse_Receptionate_Intrare_MM = 2171,
        Vizualizare_Warehouse_Receptionate_Iesire_MM = 2172,
        Vizualizare_Warehouse_Expediate_Intrare_MM = 2173,
        Vizualizare_Warehouse_Expediate_Iesire_MM = 2174,
        Vizualizare_Warehouse_Anulate_Intrare_MM = 2175,
        Vizualizare_Warehouse_Anulate_Iesire_MM = 2176,
        Vizualizare_DCG_Cheltuieli_Numerar = 2177,
        Calatori_Progrmator_Intern = 2178,
        Vizualizare_Stoc_Initial_Persoane = 2179,
        Vizualizare_Regulit_Tarifare_Expeditii = 2180,
        Calatori_Planificator_Masini = 2181,
        Vizualizare_Warehouse_Toate_MM = 2182,
        Foi_Parcurs_Vizualizare_Gps = 2183,
        Foi_Parcurs_ImportGps_Alex = 2184,
        Lista_Operatii_Manopera_Vizualizare = 2185,
        Vizualizare_Registratura_Expeditii = 2186,
        Vizualizare_LotMarfa_RegistraturaExpeditii = 2187,
        Vizualizare_Curse_RegistraturaExpeditii = 2188,
        Vizualizare_Comanda_Ferry = 2189,
        Vizualizare_FAZ_Neproductive = 2190,
        Vizualizare_Referinte_Data_Curenta = 2191,
        Vizualizare_Lot_Marfa_Rutier_MM = 2192,
        Vizualizare_Facturi_Primite_Ferry = 2193,
        Jurnal_Cumparari_NOU = 2194,
        Vizualizare_Facturi_Emise_Ferry = 2195,
        Vizualizare_Import_Tranzactii_DKV = 2196,
        Jurnal_Vanzari_Nou = 2197,
        Vizualizare_Facturi_Emise_Expeditii = 2198,
        Vizualizare_Facturi_Primite_Expeditii = 2199,
        Vizualizare_Curse_Toate_Anulate_MM = 2200,
        Vizualizare_Firme_Incasare_TVA = 2201,
        Vizualizare_Alte_Op_JurnaleTVA = 2202,
        Vizualizare_Facturi_Emise_Delamode = 2203,
        Vizualizare_TVA_Nedeductibile = 2204,
        ADGestiune_Intrare_Vizualizare = 2205,
        ADGestiune_Iesire_Vizualizare = 2206,
        Vizualizare_Camioane_Fara_DataIncarcare = 2207,
        Vizualizare_Repartizare_Costuri = 2208,
        Vizualizare_Activitate_Vanzari = 2209,
        Vizualizare_Validat_Pentru_Facturare = 2210,
        Verificare_Facturare = 2211,
        Vizualizare_OV_Sinoptic = 2212,
        Vizualizare_Obiecte_De_Inventar = 2213,
        Vizualizare_Note_Interne_Delamode = 2214,
        Vizualizare_Profitabilitate_Cursa = 2215,
        Vizualizare_ComenziInLucru_Aigle = 2216,
        Vizualizare_CurseCumulat_Aigle = 2217,
        Vizualizare_Curse_Aigle = 2218,
        Vizualizare_Profitabilitate_Cursa_Aigle = 2219,
        Vizualizare_ControlFinanciarCurse_Aigle = 2220,
        Vizualizare_FacturiEmise_Aigle = 2221,
        Vizualizare_Transporturi_Nefacturate_Aigle = 2222,
        Vizualizare_Comenzi_Toate_Aigle = 2223,
        Vizualizare_ValidatPentruFacturare_Aigle = 2224,
        Vizualizare_Partide_Facturare_Aigle = 2225,
        Vizualizare_Facturi_Proforme_Delamode = 2226,
        Vizualizare_Documente_Gestiune = 2227,
        Factura_Primita_Achizitie_VizualizareFiliala = 2228,
        Fisa_De_Magazie_Filiale = 2229,
        Vizualizare_Facturi_Comert_Filiale = 2230,
        Vizualizare_Intrari_Iesiri_Filiale = 2231,
        BC_Vizualizare_Filiale = 2232,
        NIR_Vizualizare_Filiale = 2233,
        Vizualizare_Produse_Gestiune_Filiale = 2234,
        Bon_Consum_Vizualizare_Print_Filiale = 2235,
        Vizualizare_House_Maritim = 2236,
        Vizualizare_Activitate_Vanzari_Cifra_De_Afaceri = 2237,
        Vizualizare_Solicitari_Necotate = 2238,
        Vizualizare_Documente_HR = 2239,
        Vizualizare_Firme_CRM = 2240,
        Vizualizare_Persoane_Contact_CRM = 2241,
        Vizualizare_Firme_Taskuri_CRM = 2242,
        Vizualizare_Taskuri = 2243,
        EBIDTA_Foi_Parcurs_Vizualizare = 2244,
        EBIDTA_Cheltuieli_Curse_Vizualizare = 2245,
        Curse_Facturate_Vizualizare = 2246,
        Vizualizare_Import_Anexa_Anvelope = 2247,
        Vizualizare_Decont_ParcAuto_Cheltuieli = 2248,
        Vizualizare_Import_Anexa_Anvelope_Toate = 2249,
        Vizualizare_NC_EBIDTA = 2250,
        Vizualizare_EBIDTA = 2251,
        Vizualizare_RaportPeReferinta = 2252,
        Registratura_Facturi_Emise = 2253,
        Vizualizare_Proces_Verbal_Constatare_Dunca = 2254,
        Vizualizare_Asigurari_Importante = 2255,
        Vizualizare_Proces_Verbal_Predare_Primire = 2256,
        Comanda_Client_Service_Vizualizare_Dunca = 2257,
        Deviz_Service_Vizualizare_Dunca = 2258,
		Raport_Combustibil_Dunca = 2259,
        Ruta_Dunca = 2260,
        Comanda_Client_Service_Garantie_Vizualizare = 2261,
        Vizualizare_Alimentare_Combustibil_Subcontractanti = 2262,
        Vizualizare_Carduri_Masini_Incarcare = 2263,
        Vizualizare_Bon_Consum_Combustibil = 2264,
        Vizualizare_MF_ProcesVerbal = 2265,
        Vizualizare_MF_ProcesVerbal_Detalii = 2266,
        Vizualizare_Telefoane_Detalii = 2267,
        Vizualizare_Bon_Transfer_Combustibil = 2268,
        Vizualizare_Alte_Repartizari_Costuri = 2269,
        Vizualizare_Referate_HR = 2270,
        Vizualizare_Bon_Fiscal = 2271,
        Vizualizare_InventarGestiune = 2272,
        Vizualizare_Rapoarte_TMSComercial = 2273,
        Vizualizare_Rapoarte_TMSCheltuieliFlota = 2274,
        Vizualizare_Rapoarte_FleetManagement = 2275,
        Vizualizare_Rapoarte_FinantariAsigurari = 2276,
        Vizualizare_Rapoarte_Gestiune = 2277,
        Vizualizare_Rapoarte_Service = 2278,
        Vizualizare_Rapoarte_Management = 2279,
        Vizualizare_Rapoarte_VanzareVehicule = 2280,
        Vizualizare_Miscari_Depozit = 2281,
        Vizualizare_Tarife_Depozit_Nefacturate = 2282,
        Vizualizare_Rapoarte_Contabilitate = 2283,
        #endregion Vizualizari

        #region Nomenclator

        Nomenclator = 1000,

        #endregion Nomenclator

        #region Contabilitate

        #region Contabilitate vizualizari

        FacturiActualizareViewer = 4001,
        NoteContabileViewer = 4002,
        ReevaluareViewer = 4003,
        CosturiRepartizateViewer = 4004,
        NotificareFacturiScadente = 4005,
        RenumerotareDocumente = 4006,

        #endregion Contabilitate vizualizari

        #region Contabilitate Operatii

        Note_Contabile = 3000,
        Mijloace_Fixe = 3001,
        Reevaluare = 3002,
        Registru_Jurnal = 3003,
        //Balanta_Analitica_6_Coloane_Detaliat = 3004,
        //Balanta_Analitica_4_Coloane_Detaliat_RulajDetaliat = 3005,
        Balanta_Contabila = 3006,
        //Balanta_Sintetica_4_Coloane = 3007,
        NoteContabileSalarii = 3008,
        Declaratii = 3009,
        FisaPePersoana = 3010,
        SolduriClientiFurnizori = 3011,
        BlocareDocumente = 3012,
        CentralizatorConturi = 3013,
        CentralizatorCasaBanca = 3014,
        FisaContului = 3015,
        //Balanta_Analitica_6_Coloane_Cumulat = 3016,
        //Balanta_Analitica_4_Coloane_Detaliat_RulajCumulat = 3017,
        //Balanta_Analitica_4_Coloane_Cumulat_RulajDetaliat = 3018,
        //Balanta_Analitica_4_Coloane_Cumulat_RulajCumulat = 3019,
        SolduriPePersoana = 3020,
        ConfirmariSolduriClienti = 3021,
        FisaPartener = 3022,
        Centralizare_Note_Contabile = 3023,
        Inventar_MF = 3024,
        MF_PVLuareInPrimire = 3025,
        MF_PVCasareOI = 3026,
        MF_PVCasareMFCuRecuperare = 3027,
        MF_PVCasareMFFaraRecuperare = 3028,
        ImpozitProfit = 3029,
        AlteRepartizariCosturi = 3030,
        Note_Contabile_Multiple = 3031,
        Statement_of_Account = 3032,

        #endregion Contabilitate Operatii

        #endregion Contabilitate

        #region Management

        Ponderi_Cheltuieli_Firma = 3501,
        CashFlow_Pivot = 3502,
        Cifra_Afaceri = 3503,
        Profit_Loss = 3504,
        SoldGeneral_Rentabilitate_Firma = 3505,
        Ponderi_Clienti = 3506,
        Soferi_Beneficii = 3507,
        Disponibilitati_CasaBanca = 3508,
        Grafic_Activitate = 3509,
        Evidenta_Factura = 3510,
        Evidenta_Incasari_Plati = 3511,
        Ponderi_Venituri_Firma = 3512,
        Solduri_Persoane = 3513,
        Scadente_Facturi = 3514,
        Profit_Loss_Expeditii = 3515,

        #endregion Management

        #region Calatori

        Calatori_Foi_Parcurs = 4500,
        Calatori_Transporturi = 4501,
        Calatori_Centralizator_Extern = 4502,
        Calatori_Centralizator_Intern = 4503,
        Calatori_Programare_Soferi = 4504,
        Calatori_Centralizator_Salarii = 4505,
        Calatori_Pontaj_Intern = 4506,
        Calatori_Pontaj_Transferuri = 4507,
        Calatori_Pontaj_Soferi = 4508,
        Calatori_Pontaj_International = 4509,
        Calatori_Planificator = 4510,
        SelectareSoferi = 4511,
        #endregion Calatori

        #region Expeditii

        LotMarfa = 4600,
        LotMarfa_Rutier = 4601,
        LotMarfa_Maritim = 4602,
        LotMarfa_Aerian = 4603,
        Grupaje = 4604,
        GrupajeIntroducere = 4605,
        LotMarfaIntroducere = 4606,
        CamionConstituire = 4607,
        ExpeditiiConfigurare = 4608,
        CamionConstituireDomestic = 4609,
        House_Aerian = 4610,
        AerianLotMarfaIntroducere = 4611,
        Master_Aerian = 4612,
        LotMarfaSchimbareLinie = 4613,
        MaritimLotMarfaIntroducere = 4614,
        LotMarfaIntroducereNoua = 4615,
        Operatiuni_Vamale = 4616,
        Warehouse_Introducere = 4617,
        CamionConstituireDelamode = 4618,
        Solicitari = 4619,
        Oferte = 4620,
        Warehouse_Intrare_MM = 4621,
        Warehouse_Iesire_MM = 4622,
        Warehouse_Alegere_MM = 4623,
        CMR = 4624,
        DocumenteExpeditii_Marfa = 4625,
        LotMarfaTarifare = 4626,
        Contracte = 4627,
        Documente_Camion = 4628,
        Comanda_Caraus = 4629,
        Warehouse_Alege_Document_MM = 4630,
        LotMarfa_Rutier_FTL_Project = 4631,
        CamionConstituire_FTL_Project = 4632,
        //WarehouseTreeView = 4633,
        Comanda_Ferry = 4634,
        LotMarfaVizualizare_AlegereCursa = 4635,
        LoturiMarfaVizualiazare_MM = 4636,
        WarehouseFinanciar = 4637,
        Bookings = 4638,
        LotMarfaMaritim_Delamode = 4639,
        Confirmare_Comanda = 4640,
        Vizualizare_Curse_LoturiMarfa_MM = 4641,
        OrdinColectare = 4642,
        Vizualizare_Validat_Facturare = 4643,
        BookingMaritim = 4644,
        ComandaCarausFaraContract = 4645,
        ListareEtichetaContransport = 4646,
        ListareAvizeExpeditieContransport = 4647,
        ListareAvizeLivrareContransport = 4648,
        ListareCargoManifestMM = 4649,
        RecalculExpeditii = 4650,
        NoteInterne_Delamode = 4651,
        VizualizareFacturiLotMarfaOpen = 4652,

        #endregion Expeditii

        #region Finantari

        Solduri_Contracte_Leasing = 4700,

        #endregion Finantari

        #region CRM

        Firme_CRM = 4800,

        #endregion

        #region Management Anvelope

        ManagementAnvelope = 4900,
        OperatiuniAnvelope = 4901,

        #endregion

        #region Export date
        ExportDate = 4902,
        ExportDateFormMain = 4904,
        #endregion

        Nomenclator_Clasificari_Asignare_Coloane_Planificator = 4903,

        #region Dolotrans
        ImportCumparareVanzareVehicule_Wizard = 5000,
        #endregion Dolotrans
        


        #region UI negative ptr rapoarte
        ConfirmariSolduriClientiFurnizori = -1,
        RaportJurnalCumparariCumulat = -2,
        RaportJurnalCumparariFaraTVALaIncasare = -3,
        RaportJurnalCumparariCuTVALaIncasare = -4,
        RaportJurnalVanzari = -5,
        FisaPartenerPtConditieContEchLei = -6,
        RaportJurnalRectificativ = -8,
        RaportPVPredareCarduriCombustibil = -9,
        RaportPVPrimireCarduriCombustibil = -10,
        RaportIstoricPretAchizitie = -11,
        RaportActiuniCursePlanificator = -12,
        #endregion

        #region Scadente
        PanouriScadente = 6000,
        PanouriScadenteVizualizare = 6001,
        #endregion

        TrimiteEmailCarausi = -7,
    }

    public enum ConfirmationResult
    {
        Executed,
        Failed,
        ExecutedWithWarnings,
        FailedUnknownError,
        Canceled
    }

    public enum Nomenclator_Puncte_Service
    {
        CDA = 1,
        LKW = 2,
        Terti = 3
    }

    public enum ExecutionResult
    {
        Executed,
        Failed,
        ExecutedWithWarnings,
        FailedUnknownError
    }

    public enum OperationResult
    {
        Executed,
        Failed,
        ExecutedWithWarnings,
        FailedUnknownReason,
        FailedOnConfirmation,
        FailedOnExecution,
        FailedConcurrencyUpdate,
        FailedConcurrencyUpdateRecordDeleted,
        FailedExceptionFromOutside,
        CanceledOnConfirmation
    }

    public enum MessageType : byte
    {
        Error = 0,
        Warning = 1,
        Message = 2
    }

    public enum OperationType : byte
    {
        NewRecord = 0,
        OpenRecord = 1,
        SaveRecord = 2,
        BlockRecord = 3,
        CancelRecord = 4,
        ActivateRecord = 5,
        DeleteRecord = 6,
        PrintRecord = 7,
        SilentPrint = 8,
        Revoke = 9
    }

    public enum RecordState : byte
    {
        Unsaved = 0,//caller = introducere, nesalvat
        Active = 1,
        Blocked = 2,
        Cancelled = 3
    }

    public enum RecordGeneratorType : int
    {
        Undefined = 0,
        ChitantaFromFactura = 1,
        WindnetLog = 2,
    }

    public enum SearchInterfaceType : int
    {
        Undefined = 0,
        [EnumDescription("Facturi emise - operational - Selectie inregistrare")]
        Factura_Emisa_Operational = 1,
        [EnumDescription("Facturi primite - operational - Selectie inregistrare")]
        Factura_Primita_Caraus = 2,
        [EnumDescription("Facturi primite - alte facturi - Selectie inregistrare")]
        Factura_Primita_Altele = 3,
        [EnumDescription("Utilizator - Selectie inregistrare")]
        Utilizator = 4,
        [EnumDescription("Grup utilizatori - Selectie inregistrare")]
        Grup_Utilizatori = 5,
        [EnumDescription("Facturi emise - alte facturi - Selectie inregistrare")]
        Factura_Emisa_Altele = 6,
        [EnumDescription("Facturi emise - storno - Selectie inregistrare")]
        Factura_Emisa_Storno = 7,
        [EnumDescription("Facturi emise - sold initial - Selectie inregistrare")]
        Factura_Emisa_SoldInitial = 8,
        [EnumDescription("Facturi primite - sold initial - Selectie inregistrare")]
        Factura_Primita_SoldInitial = 9,
        [EnumDescription("Chitanta - Selectie inregistrare")]
        Chitanta = 10,
        [EnumDescription("Facturi primite - storno - Selectie inregistrare")]
        Factura_Primita_Storno = 11,
        [EnumDescription("Note contabile - Selectie inregistrare")]
        Note_Contabile = 12,
        [EnumDescription("Constituire curse - Selectie inregistrare")]
        Curse_Constituire = 13,
        [EnumDescription("Alte operatiuni purtatoare de TVA")]
        AlteOpJurnaleTVA = 14,
        [EnumDescription("Registrul de banca")]
        Banca = 15,
        [EnumDescription("Fisa concediu angajati")]
        Angajati_Concedii = 16,
        [EnumDescription("Registrul de casa")]
        Casa = 17,
        [EnumDescription("Comenzi predefinite")]
        Comenzi_Predefinite = 18,
        [EnumDescription("Comenzi in lucru")]
        Comenzi = 19,
        [EnumDescription("Firme")]
        Firme = 20,
        [EnumDescription("Fisa tehnica vehicule")]
        Fisa_Tehnica_Masini_Proprii = 21,
        [EnumDescription("Reparatii/Regie auto")]
        Cheltuieli_Parc_Auto = 22,
        [EnumDescription("Fisa angajat")]
        Fisa_Angajati = 23,
        [EnumDescription("CEC / BO")]
        CEC = 24,
        [EnumDescription("Daune")]
        Daune = 25,
        [EnumDescription("Decont terti")]
        Decont_Terti = 26,
        [EnumDescription("Inventare")]
        Inventare = 27,
        [EnumDescription("Fisa tehnica aeronave")]
        Fisa_Tehnica_Aeronave = 28,
        [EnumDescription("Decont cheltuieli cursa")]
        Decont_Cursa_General = 29,
        [EnumDescription("Voyage report")]
        Voyage_Report = 30,
        [EnumDescription("Foaie de parcurs / Consum pe tronsoane")]
        Foaie_Parcurs_Consum_Tronsoane = 31,
        [EnumDescription("Foaie de parcurs")]
        Foaie_Parcurs = 32,
        [EnumDescription("Curse evenimente")]
        Curse_Evenimente = 33,
        [EnumDescription("NIR")]
        NIR = 34,
        [EnumDescription("Aviz insotire marfa iesire")]
        Aviz_Insotire_Marfa_Iesire = 35,
        [EnumDescription("Aviz insotire marfa intrare")]
        Aviz_Insotire_Marfa_Intrare = 36,
        [EnumDescription("Bon de consum")]
        Bon_De_Consum = 37,
        [EnumDescription("Facturi primite - achizitie - Selectie inregistrare")]
        Factura_Primita_Achizitie = 38,
        [EnumDescription("Lista produse")]
        Lista_Produse = 39,
        [EnumDescription("Bon de transfer")]
        Bon_Transfer = 40,
        [EnumDescription("Dispozitii de plata / incasare")]
        Dispozitii_Plata_Incasare = 41,
        [EnumDescription("Alte documente gestiune - iesire")]
        Alte_Documente_Gestiune = 42,
        [EnumDescription("Evidenta carduri")]
        Evidenta_Carduri = 43,
        [EnumDescription("Comenzi service")]
        Comanda_Client_Service = 44,
        [EnumDescription("Deviz service")]
        Deviz = 45,
        [EnumDescription("Fisa tehnica masini terti")]
        Fisa_Tehnica_Masini_Terti = 46,
        [EnumDescription("Facturi emise - comert - Selectie inregistrare")]
        Factura_Emisa_Comert = 47,
        [EnumDescription("Fisa tehnica masini carausi")]
        Masini_Carausi = 48,
        [EnumDescription("Facturi emise - service - Selectie inregistrare")]
        Factura_Emisa_Service = 49,
        [EnumDescription("Inchidere / Avansuri")]
        Inchidere_Avansuri = 50,
        [EnumDescription("Procese verbale de compensare")]
        PV_Compensare = 51,
        [EnumDescription("Finantari")]
        Finantari = 52,
        [EnumDescription("Asigurari")]
        Asigurari = 53,
        [EnumDescription("Mijloace fixe si obiecte de inventar")]
        MF_Obiecte_De_Inventar_Firma = 54,
        [EnumDescription("Carnete TIR")]
        Carnete_TIR = 55,
        [EnumDescription("Autorizatii")]
        Autorizatii = 56,
        [EnumDescription("Angajati caziere")]
        Angajati_Caziere = 57,
        [EnumDescription("Alte documente gestiune - intrare")]
        Alte_Documente_Gestiune_Intrare = 58,
        [EnumDescription("Comercial - oferte")]
        Comercial_Oferte = 59,
        [EnumDescription("Service - deviz estimativ")]
        DevizEstimativ = 60,
        [EnumDescription("Service - licitatii produse")]
        LicitatiiProduse = 61,
        [EnumDescription("Facturi primite - sosite - Selectie inregistrare")]
        Factura_Primita_Sosite = 62,
        [EnumDescription("Integrare comanda in cursa existenta")]
        Integrare_Cmd_In_Cursa_Existenta = 63,
        [EnumDescription("Proces verbal transfer stocuri persoane")]
        PV_Transfer_Stocuri_Persoane = 64,
        [EnumDescription("Adeverinte salariati")]
        Angajati_Adeverinta = 65,
        [EnumDescription("Transfer avansuri")]
        Transfer_Avansuri = 66,
        [EnumDescription("Constituire curse - Selectie inregistrare")]
        Calatori_Curse_Constituire = 67,
        [EnumDescription("Foi parcurs - Selectie inregistrare")]
        Calatori_Foi_Parcurs = 68,
        [EnumDescription("Juridic - Dosar")]
        Juridic_Asiguratori = 69,
        [EnumDescription("Grupaje")]
        Grupaje = 70,
        [EnumDescription("Lot marfa rutier")]
        LotMarfa_Rutier = 71,
        [EnumDescription("Lot marfa aerian")]
        LotMarfa_Aerian = 72,
        [EnumDescription("Lot marfa maritim")]
        LotMarfa_Maritim = 73,
        [EnumDescription("Fisa alimentare motorina")]
        Fisa_Alimentare_Motorina = 74,
        [EnumDescription("Calatori comenzi")]
        Calatori_Transporturi = 75,
        [EnumDescription("House - aerian")]
        House_Aerian = 76,
        [EnumDescription("Operatiuni vamale")]
        Operatiuni_Vamale = 77,
        [EnumDescription("Warehouse")]
        Warehouse = 78,
        [EnumDescription("Juridic - Somatii de plata")]
        Juridic_Somatii = 79,
        [EnumDescription("Juridic - Executari silite")]
        Juridic_Executari = 80,
        [EnumDescription("Juridic - Decizii de concediere")]
        Juridic_DeciziiConcediere = 81,
        [EnumDescription("Juridic - Dosar avocati")]
        Juridic_DosarAvocati = 82,
        [EnumDescription("Warehouse intrare")]
        Warehouse_Intrare = 83,
        [EnumDescription("Warehouse iesire")]
        Warehouse_Iesire = 84,
        [EnumDescription("CMR")]
        CMR = 85,
        [EnumDescription("Expeditii - tarifare")]
        LotMarfaExpeditiiTarifare = 86,
        [EnumDescription("Solicitari")]
        Solicitari = 87,
        [EnumDescription("Oferte")]
        Oferte = 88,
        [EnumDescription("Lista incarcare")]
        ListaIncarcare = 89,
        [EnumDescription("Lista descarcare")]
        ListaDescarcare = 90,
        [EnumDescription("Loading list")]
        LoadingList = 91,
        [EnumDescription("Warehouse aviso")]
        WarehouseAviso = 92,
        [EnumDescription("Unloading list")]
        UnloadingList = 93,
        [EnumDescription("Truck manifest")]
        TruckManifest = 94,
        [EnumDescription("Aviz de Insotire a Marfii")]
        AvizInsotireMarfa = 95,
        [EnumDescription("Comanda de caraus")]
        ComandaCaraus = 96,
        [EnumDescription("Foaie de parcurs / Consum pe tronsoane SPLIT")]
        Foaie_Parcurs_Consum_Tronsoane_Split = 97,
        [EnumDescription("Contracte")]
        Contracte = 98,
        [EnumDescription("Lot marfa rutier FTL Project")]
        LotMarfa_Rutier_FTL_Project = 99,
        [EnumDescription("Curse FTL Project")]
        Curse_Constituire_FTLProject = 100,
        [EnumDescription("Licitatii produse service")]
        Licitatii_Produse_Toate = 101,
        [EnumDescription("Operatii manopera - Service")]
        Denumire_Operatie = 102,
        [EnumDescription("Registratura intrari - expeditii")]
        RegistraturaExpeditiiIntrari = 103,
        [EnumDescription("Comanda Ferry")]
        Comanda_Ferry = 104,
        [EnumDescription("FAZ Neproductive")]
        FAZ_Neproductive = 105,
        [EnumDescription("Programator intern")]
        Calatori_Pontaj_Soferi = 106,
        [EnumDescription("Bookings")]
        Bookings = 107,
        [EnumDescription("Confirmare de transport")]
        ConfirmareTransport = 108,
        [EnumDescription("Facturi proforme")]
        FacturaProforma = 109,
        [EnumDescription("Ordin de colectare")]
        OrdinColectare = 110,
        [EnumDescription("Insolventa")]
        Insolventa = 111,
        [EnumDescription("CIM")]
        CIM = 112,
        [EnumDescription("Notificare")]
        Notificare = 113,
        [EnumDescription("Decizie incetare CIM art 31")]
        DecizieIncetareArt31 = 114,
        [EnumDescription("Decizie incetare CIM art 55")]
        DecizieIncetareArt55 = 115,
        [EnumDescription("Decizie incetare CIM art 65")]
        DecizieIncetareArt65 = 116,
        [EnumDescription("Decizie incetare CIM art 56")]
        DecizieIncetareArt56 = 117,
        [EnumDescription("Decizie incetare CIM art 81")]
        DecizieIncetareArt81 = 118,
        [EnumDescription("Notificare Decizie Imputare telefoane")]
        Notificare_Decizie_Imputare_Telefoane = 119,
        [EnumDescription("Referat Cercetare Disciplinara")]
        Referat_Cercetare_Disciplinara = 120,
        [EnumDescription("Referat Depasire Motorina")]
        Referat_Depasire_Motorina = 121,
        [EnumDescription("Proces Verbal de Constatare")]
        Proces_Verbal_Constatare_Dunca = 122,
        [EnumDescription("Proces Verbal de Predare Primire")]
        Proces_Verbal_Predare_Primire = 123,
        [EnumDescription("Inventar gestiune")]
        InventarGestiune = 124,
        [EnumDescription("Ruta")]
        Ruta = 125,
        [EnumDescription("Management anvelope")]
        ManagementAnvelope = 126,
        [EnumDescription("Operatiuni anvelope")]
        OperatiuniAnvelope = 127,
        [EnumDescription("Inventar mijloace fixe")]
        InventarMF = 128,
        [EnumDescription("FORMULAR DE APROVIZIONARE")]
        Formular_Achizitii = 129,
        [EnumDescription("Facturi primite - piese reconditionate - Selectie inregistrare")]
        Factura_Primita_Piese_Reconditionate = 130,
        [EnumDescription("Proces verbal de luare in primire")]
        MF_PVLuareInPrimire = 131,
        [EnumDescription("Proces verbal de casare obiecte de inventar")]
        MF_PVCasareOI = 132,
        [EnumDescription("Proces verbal de casare mijloc fix cu recuperare de produse")]
        MF_PVCasareMFCuRecuperare = 133,
        [EnumDescription("Proces verbal de casare mijloc fix fara recuperare de produse")]
        MF_PVCasareMFFaraRecuperare = 134,
        [EnumDescription("Bon de transfer combustibil")]
        Bon_Transfer_Combustibil = 135,
        [EnumDescription("Alte repartizari costuri")]
        AlteRepartizariCosturi = 136,
        [EnumDescription("Referat R.C.A - Malus")]
        ReferatRCAMalus = 137,
        [EnumDescription("Referat de avertisment consum")]
        ReferatAvertismentConsum = 138,
        [EnumDescription("Referat de depasire viteza")]
        ReferatDepasireViteza = 139,
        [EnumDescription("Referat de constatare si evaluare a pagubelor")]
        ReferatConstatareEvaluarePagube = 140,
        [EnumDescription("Inventar combustibil masini")]
        InventarCombustibilMasini = 141,
        [EnumDescription("Bon fiscal")]
        BonFiscal = 142,
        [EnumDescription("Bon de consum combustibil")]
        Bon_Consum_Combustibil = 143,
        [EnumDescription("Acte aditionale pentru CIM")]
        Angajati_ActAditional = 144,
        [EnumDescription("Notificari incetare pentru CIM")]
        Angajati_Notificare_Incetare = 145,
        [EnumDescription("Decizii incetare pentru CIM")]
        Angajati_Decizie_Incetare = 146,
        [EnumDescription("Decizii colective salariale")]
        Angajati_DecizieColectivaSalariala = 147,
        [EnumDescription("Note de lichidare pentru CIM")]
        Angajati_NotaLichidare = 148,
        [EnumDescription("Firme contracte")]
        FirmeContracte = 149,
        [EnumDescription("Referat")]
        ReferatImputareAmenda = 150,
        [EnumDescription("Referat")]
        ReferatDisciplinar = 151,
        [EnumDescription("Facturi emise - avize - Selectie inregistrare")]
        Factura_Emisa_Avize = 152,
        [EnumDescription("Adrese de livrare")]
        FirmeAdreseLivrare = 153,
        [EnumDescription("RaportEveniment")]
        RaportEveniment = 154,
    }

    public enum TipRepartizareVenit : byte
    {
        FacturaEmisa = 1,
        AlteCheltuieli_Decont = 2,
        AlteOpJurnaleTVA = 3,
        Casa = 4,
        Banca = 5,
        NoteContabile = 6,
        CheltuieliParcAuto = 7,
    }

    public enum TipRepartizareCosturi : byte
    {
        FacturaPrimita = 1,
        Casa = 2,
        Banca = 3,
        NoteContabile = 4,
        Deconturi = 5,
        AlteOpJurnaleTVA = 6,
        CheltuieliParcAuto = 7,
        BonConsum = 8,
        AlteDocumenteGestiune = 9,
        AlteRepartizariCosturi = 10,
        CheltuieliDiverse = 11
    }

    public enum CustomOperation
    {
        [EnumDescription("Interfata cautare")]
        SearchInterface = 1,
        [EnumDescription("Repartizare venituri")]
        RepartizareVenituri = 101,
        [EnumDescription("Repartizare costuri")]
        RepartizareCosturi = 102,
        [EnumDescription("Facturare operatiuni")]
        PartideFacturare = 103,
        [EnumDescription("Facturare curse")]
        CurseFacturare = 104,
        [EnumDescription("ActualizareFacturi")]
        ActualizareFacturi = 105,
        [EnumDescription("Generare facturi emise")]
        GenerareFacturiTransporturi = 106,
        [EnumDescription("Import combustibil pompa")]
        ImportCombustibilPompa = 107,
        [EnumDescription("Actualizare produse stoc")]
        ActualizareProduseStoc = 108,
        [EnumDescription("Schimbare parola utilizator")]
        ChangePassword = 2,
        [EnumDescription("Atasamente")]
        Atasamente = 109,
        [EnumDescription("Evidenta paleti partide")]
        EvidentaPartidePaleti = 110,
        [EnumDescription("Foi concediu active")]
        FoiConcediuActive = 111,
        [EnumDescription("Ordin de plata")]
        OrdinDePlata = 112,
        [EnumDescription("Import combustibil card")]
        ImportCombustibilCard = 113,
        [EnumDescription("Facturi neincasate")]
        Facturi_Neincasate = 114,
        [EnumDescription("Facturi neplatite")]
        Facturi_Neplatite = 115,
        [EnumDescription("Devize facturare")]
        DevizeFacturare = 116,
        [EnumDescription("Import combustibil card cheltuiala cursa")]
        ImportCombustibilCard_CheltuialaCursa = 117,
        [EnumDescription("Import combustibil pompa cheltuiala cursa")]
        ImportCombustibilPompa_CheltuialaCursa = 118,
        [EnumDescription("Reevaluare solduri clienti/furnizori")]
        Reevaluare = 119,
        [EnumDescription("Curs valutar")]
        CursValutar = 120,
        [EnumDescription("Vizualizare stoc zilnic")]
        VizualizareStocZilnic = 121,
        [EnumDescription("Centralizator devize")]
        CentralizatorDevize = 122,
        [EnumDescription("Registru jurnal analitic/sintetic")]
        Registru_Jurnal = 123,
        [EnumDescription("Compensare")]
        Compensare = 124,
        [EnumDescription("Decont cursa avansuri resturi")]
        DecontCursaAvansuriResturi = 125,
        [EnumDescription("Compensare - actualizare facturi")]
        PV_Compensare_Actualizare_Facturi = 126,
        [EnumDescription("Actualizare CEC-uri")]
        ActualizareCEC = 127,
        [EnumDescription("Foaie parcurs comenzi")]
        FoaieParcursComenzi = 128,
        [EnumDescription("Import salarii")]
        ImportSalarii = 129,
        [EnumDescription("Note contabile salarii")]
        NoteContabileSalarii = 130,
        [EnumDescription("Generare facturi primite combustibil card")]
        GenerareFacturiCombustibilCard = 131,
        [EnumDescription("Declaratii")]
        Declaratii = 132,
        [EnumDescription("Fisa pe persoana")]
        FisaPePersoana = 133,
        [EnumDescription("Solduri clienti / furnizori")]
        SolduriClientiFurnizori = 134,
        [EnumDescription("Repartizari contracte leasing")]
        ContractLeasing = 135,
        [EnumDescription("Actualizare daune")]
        ActualizareDaune = 136,
        [EnumDescription("Blocare documente")]
        BlocareDocumente = 137,
        [EnumDescription("Centralizare conturi")]
        CentralizatorConturi = 138,
        [EnumDescription("Centralizare casa banca")]
        CentralizatorCasaBanca = 139,
        [EnumDescription("Anexa factura importata combustibil card")]
        AnexaCombustibilCardFacturi = 140,
        [EnumDescription("Firme CIF invalid")]
        FirmeCIFInvalid = 141,
        [EnumDescription("Aviz - generare NIR")]
        AvizGenerareNIR = 142,
        [EnumDescription("Recuperare TVA din facturi")]
        FacturiRecuperareTVA = 143,
        [EnumDescription("Templates")]
        Templates = 144,
        [EnumDescription("Fisa contului")]
        FisaContului = 145,
        [EnumDescription("Calcul profit curse")]
        Curse_Calcul_Profit = 146,
        [EnumDescription("Devize first truck")]
        Devize_First_Truck = 147,
        [EnumDescription("Culori date scadente")]
        ScadenteColors = 148,
        [EnumDescription("Curs valutar online")]
        CursValutarOnline = 149,
        [EnumDescription("Actualizare asigurari")]
        ActualizareAsigurari = 150,
        [EnumDescription("Serii produse - bon consum")]
        BonConsumSeriiProduse = 151,
        [EnumDescription("ImportComenziEDI")]
        ImportComenziEDI = 152,
        [EnumDescription("ImportCombustbilCardDecont")]
        ImportCombustbilCardDecont = 153,
        [EnumDescription("Decont Cursa - diurna")]
        DecontCursaDiurna = 154,
        [EnumDescription("Grafic activitate - transporturi")]
        GraifActivitateTransporturi = 155,
        [EnumDescription("Comenzi Cross-Docking")]
        TransporturiCrossDocking = 156,
        [EnumDescription("NIR - Facturi Sosite")]
        NIRFacturiSosite = 157,
        [EnumDescription("Templates - Administrare")]
        TemplatesAdministration = 158,
        [EnumDescription("NIR generare Bon Consum")]
        NIRGenerareBonConsum = 159,
        [EnumDescription("Facturi primite cu TVA Neexigibil")]
        FacturiPrimiteNeexigibile = 160,
        [EnumDescription("Culori perioade scadente")]
        ScadentePeriodColors = 161,
        [EnumDescription("Programari utilaje in service")]
        ProgramariUtilajeService = 162,
        [EnumDescription("Programari mecanici in service")]
        ProgramariMecaniciService = 163,
        [EnumDescription("Actualizari deconturi")]
        ActualizareDeconturi = 164,
        [EnumDescription("Actualizare stoc initial persoane")]
        ActualizareStocInitialPersoane = 165,
        [EnumDescription("Actualizare stoc persoane")]
        ActualizareStocuriPersoane = 166,
        [EnumDescription("Generare bon consum din combustibil pompa")]
        PompaGenerareBonConsum = 167,
        [EnumDescription("Diurna intern - extern")]
        DecontDiurnaInternExtern = 168,
        [EnumDescription("Management importuri combustibil")]
        ManagementImporturiCombustibil = 169,
        [EnumDescription("Centralizator operatii mecanici")]
        CentralizatorOperatiiMecanici = 170,
        [EnumDescription("Comenzi - validare pentru facturare")]
        ValidarePentruFacturare = 171,
        [EnumDescription("Import comenzi LINDE")]
        ImportComenziLinde = 172,
        [EnumDescription("EBIDTA pe camion")]
        EBIDTACamion = 173,
        [EnumDescription("Lot marfa - grupaje")]
        LotMarfaXGrupaje = 174,
        [EnumDescription("Balanta mijloace fixe")]
        BalantaMijloaceFixe = 175,
        [EnumDescription("Lot marfa - curse")]
        LotMarfaXCurse = 176,
        [EnumDescription("Grupaj - curse")]
        GrupajXCurse = 177,
        [EnumDescription("Lot marfa operational - curse")]
        LotMarfaXCurseOperational = 178,
        [EnumDescription("Lot marfa operational - grupaje")]
        LotMarfaXGrupajeOperational = 179,
        [EnumDescription("Grupaj operational - curse")]
        GrupajXCurseOperational = 180,
        [EnumDescription("EDI - loader")]
        EDILoader = 181,
        [EnumDescription("EDI - generare firme de livrare")]
        EDIGeneratorFirme = 182,
        [EnumDescription("P&G - comenzi")]
        ComenziPG = 183,
        [EnumDescription("Serii produse atasate - bon consum")]
        BonConsumSeriiProduseAtasate = 184,
        [EnumDescription("Foaie parcurs - calcul consumuri")]
        FoaieParcursCalculAdBlue = 185,
        [EnumDescription("Stingere facturi sosite")]
        StingereFacturiSosite = 186,
        [EnumDescription("Import comenzi EWALS")]
        ImportComenziEwals = 187,
        [EnumDescription("Stingere facturi nefacturate")]
        StingereFacturiNefacturate = 188,
        [EnumDescription("Decont cursa avansuri resturi complex")]
        DecontCursaAvansuriResturiComplex = 189,
        [EnumDescription("Expeditii - reguli tarifare")]
        LotMarfaReguliTarifare = 190,
        [EnumDescription("Lot marfa - calcul tarif")]
        LotMarfaCalculTarif = 191,
        [EnumDescription("Oferte - genereaza lot marfa")]
        GenereazaLotMarfaOferte = 192,
        [EnumDescription("Camion - generare CMR")]
        CamionGenerareCMR = 193,
        [EnumDescription("Nota comanda - gestionare produse")]
        NotaComandaProduse = 194,
        [EnumDescription("Camion - generare document")]
        CamionGenerareDocument = 195,
        [EnumDescription("Vizualizare note comenzi atasate")]
        VizualizareNoteComenziAtasate = 196,
        [EnumDescription("Lot marfa - Calcul Tarife")]
        LotMarfaTarifar = 197,
        [EnumDescription("Vizualizare stoc zilnic pe persoane")]
        VizualizareStocPersoane = 198,
        [EnumDescription("Solduri pe persoana")]
        SolduriPePersoana = 199,
        [EnumDescription("Curse expeditii facturare")]
        CurseExpeditiiFacturare = 200,
        [EnumDescription("Foi parcurs split")]
        FoiParcursXSplit = 201,
        [EnumDescription("Vizualizare comenzi atasate la tronson")]
        FoiParcursComenziVizualizare = 202,
        [EnumDescription("Alimentari combustibil tancuri")]
        AlimentariCombustibilTancuri = 203,
        [EnumDescription("Vizualizare log-uri WindNet")]
        ViewLogs = 204,
        [EnumDescription("Vizualizare istoric log-uri WindNet")]
        ViewLogsHistory = 205,
        [EnumDescription("Fisa magazie")]
        FisaMagazie = 206,
        [EnumDescription("Referinte - registratura")]
        RegistraturaExpeditiiReferinte = 207,
        [EnumDescription("Comenzi client - necesar de comandat")]
        NecesarComandaClient = 208,
        [EnumDescription("Loturi / Curse - facturi emise si primite")]
        LoturiCurseFacturiEmisePrimite = 209,
        [EnumDescription("Preavizari - loturi marfa")]
        LotMarfaPreavizariDelamode = 210,
        [EnumDescription("Bun plata facturi primite")]
        ExpeditiiFacturaPrimitaBunPlata = 211,
        [EnumDescription("Registratura facturi emise - opis documente")]
        RegistraturaExpeditiiOpisDocumente = 212,
        [EnumDescription("Import comenzi ferry")]
        ImportComenziFerry = 213,
        [EnumDescription("Confirmari solduri clienti")]
        ConfirmariSolduriClienti = 214,
        [EnumDescription("Import tranzactii DKV")]
        ImportTranzactiiDKV = 215,
        [EnumDescription("EBIDTA pe camioane")]
        EBIDTACamionAlex = 216,
        [EnumDescription("Comenzi Ferry Generare FE")]
        ComenziFerryGenerareFacturi = 217,
        [EnumDescription("Depozit - preavizari - Delamode")]
        WarehousePreavizariDelamode = 218,
        [EnumDescription("Merge tabele")]
        MergeTabele = 219,
        [EnumDescription("Centralizator balante si solduri initiale")]
        CentralizatorBalanteSolduri = 220,
        [EnumDescription("Bookings")]
        Bookings = 221,
        [EnumDescription("Transporturi - predare documente")]
        TransporturiPredareDocumente = 222,
        [EnumDescription("Firme - incasare TVA")]
        FirmeIncasareTVA = 223,
        [EnumDescription("Jurnal cumparari Nou")]
        JurnalCumparariNou = 224,
        [EnumDescription("Marfuri EDI Kaercher")]
        KaercherEDI = 225,
        [EnumDescription("Jurnal de vanzari Nou")]
        JurnalVanzariNou = 226,
        [EnumDescription("Sold")]
        SoldulLaData = 227,
        [EnumDescription("Marfuri EDI Pallex")]
        PallexEDI = 228,
        [EnumDescription("Fisa partener")]
        FisaPartener = 229,
        [EnumDescription("Centralizare note contabile")]
        CentralizareNoteContabile = 230,
        [EnumDescription("Raport operational")]
        RaportOperationalExpeditii = 231,
        [EnumDescription("Expeditii - cifa afaceri")]
        CifraAfaceriExpeditii = 232,
        [EnumDescription("EDI - status reports")]
        EDIStatusReports = 233,
        [EnumDescription("Comenzi Ferry - import preturi")]
        ImportPreturiFerry = 234,
        [EnumDescription("Import anexe asigurari")]
        ImportAnexaAsigurari = 235,
        [EnumDescription("Firme - reevaluare TVA")]
        FirmeReevaluareTVA = 236,
        [EnumDescription("Vizualizare scadente operational")]
        VizualizareScadenteOperational = 237,
        [EnumDescription("Rentabilitate anvelope")]
        Rentabilitate_Anvelope = 238,
        [EnumDescription("Distante orase Romania")]
        DistanteOraseRomania = 239,
        [EnumDescription("Raport operational domestic")]
        RaportOperationalExpeditiiDomestic = 240,
        [EnumDescription("Raport operational FTL Project")]
        RaportOperationalExpeditiiFTLProject = 241,
        [EnumDescription("Vizualizare camioane fara data de incarcare")]
        Vizualizare_Camioane_Fara_DataIncarcare = 242,
        [EnumDescription("Firma credit control")]
        Firma_Credit_Control = 243,
        [EnumDescription("Raport operational financiar")]
        RaportOperationalFinanciarExpeditii = 244,
        [EnumDescription("Reevaluare Casa / Banca")]
        ReevaluareCasaBanca = 245,
        [EnumDescription("Import devize")]
        Calatori_Import_Devize = 246,
        [EnumDescription("Solduri contracte leasing")]
        Solduri_Contracte_Leasing = 247,
        [EnumDescription("Bonuri transfer - importuri pompa")]
        BonTransferImporturiPompa = 248,
        [EnumDescription("Import facturi primite")]
        ImportFacturiElion = 249,
        [EnumDescription("Profit si pierdere")]
        RaportProfitLossDetaliat = 250,
        [EnumDescription("truck & trace documente")]
        TruckTraceExpeditii = 251,
        [EnumDescription("Reevaluare finantari")]
        ReevaluareFinantari = 252,
        [EnumDescription("EBIDTA autocare")]
        EBIDTAAutocar = 253,
        [EnumDescription("Estimari costuri curse")]
        EstimariCosturiCurse = 254,
        [EnumDescription("Lot marfa venituri - validare pentru facturare")]
        ValidareExpeditiiPentruFacturare = 255,
        [EnumDescription("Vizualizare validat pentru facturare")]
        Vizualizare_Validat_Facturare = 256,
        [EnumDescription("Vizualizare facturi in sold")]
        VizualiareFacturiInSold = 257,
        [EnumDescription("Vizualizare facturi emise client")]
        VizualizareFacturiEmiseClient = 258,
        [EnumDescription("Vizualizare facturi emise client")]
        NotificareFacturiScadente = 259,
        [EnumDescription("Fisa cont noua")]
        FisaContNoua = 260,
        [EnumDescription("Vizualizare facturi emise juridic")]
        Vizualizare_Facturi_Emise_Juridic = 261,
        [EnumDescription("Raport operational aerian si maritim")]
        RaportOperationalAerianMaritim = 262,
        [EnumDescription("Reevaluare solduri persoane")]
        ReevaluareSolduriPersoane = 263,
        [EnumDescription("Import Edi Italia")]
        ImportEdiStanteSevsta = 264,
        [EnumDescription("Reevaluare solduri alte conturi in valuta")]
        ReevaluareSolduriConturiValuta = 265,
        [EnumDescription("Pontaj mecanici in timp real")]
        PontajMecaniciTimpReal = 266,
        [EnumDescription("Lot marfa vizualizare")]
        LotMarfaVizualizare = 267,
        [EnumDescription("Situatii contabile")]
        SituatiiContabile = 268,
        [EnumDescription("Fisa de magazie vizualizare")]
        FisaDeMagazieVizualizare = 269,
        [EnumDescription("Fisa de magazie")]
        FisaDeMagazie = 270,
        [EnumDescription("Warehouse Tree View Delamode")]
        WarehouseTreeViewDelamode = 271,
        //[EnumDescription("Forecast")]
        //Forecast = 272,
        [EnumDescription("Incasari VS Plati Detaliat MM")]
        EvidentaIncasariPlatiDetaliatMM = 273,
        //[EnumDescription("Forecast vizualizare")]
        //ForecastVizualizare = 274,
        [EnumDescription("Import pontaj extern")]
        Calatori_Import_Pontaj_Extern = 275,
        //[EnumDescription("Statistici DTD")]
        //StatisticiDTD = 276,
        [EnumDescription("Manager raport EBIDTA")]
        ManagerRaportEBIDTA = 277,
        [EnumDescription("Validare facturare - operatiuni vamale")]
        ValidareFacturare_OperatiuniVamale = 278,
        [EnumDescription("Validat facturare - operatiuni vamale")]
        ValidatFacturare_OperatiuniVamale = 279,
        [EnumDescription("Validare facturare - warehouse")]
        ValidareFacturare_Warehouse = 280,
        [EnumDescription("Validat facturare - warehouse")]
        ValidatFacturare_Warehouse = 281,
        [EnumDescription("Inventar stoc initial")]
        StocInitialInventar = 282,
        [EnumDescription("Registratura curse vizualizare")]
        RegistraturaCurseVizualizare = 283,
        //[EnumDescription("Raport Cost/Palet/Kg")]
        //CostPerPaletPerKg = 284,
        [EnumDescription("ExportEdiUK")]
        ExportEdiUK = 285,
        [EnumDescription("Raport operational FTL Project loturi marfa")]
        RaportOperationalExpeditiiFTLProjectLoturiMarfa = 286,
        [EnumDescription("Repartizare filiale")]
        RepartizareFiliale = 287,
        [EnumDescription("Repartizare filiale detalii")]
        RepartizareFilialeDetalii = 288,
        [EnumDescription("Angajati pontaj")]
        AngajatiPontaj = 289,
        //[EnumDescription("Contabilitate vs. raport operational")]
        //ContabilitateVsRaportOperational = 290,
        //[EnumDescription("Contabilitate vs. raport operational detaliat")]
        //ContabilitateVsRaportOperationalDetaliat = 291,
        [EnumDescription("Vizualizare note contabile salarii")]
        VizualizareNoteContabileSalarii = 292,
        [EnumDescription("Profit & Loss Delamode")]
        Profit_Loss_Delamode = 293,
        [EnumDescription("Provizioane")]
        Provizioane = 294,
        [EnumDescription("Expeditii - cifa afaceri detaliat")]
        CifraAfaceriExpeditiiDetaliat = 295,
        [EnumDescription("Foaie parcurs comenzi")]
        FoaieParcursComenziNew = 296,
        [EnumDescription("Vizualizare note contabile")]
        Vizualizare_Note_Contabile_Toate = 297,
        [EnumDescription("Notificare facturi scadente - filtare")]
        NotificareFacturiScadenteFiltrare = 298,
        [EnumDescription("Repartizare centre profit")]
        RepartizareCentreProfit = 299,
        [EnumDescription("Repartizare centre profit detalii")]
        RepartizareCentreProfitDetalii = 300,
        [EnumDescription("Centralizator pontaj")]
        CentralizatorPontaj = 301,
        //[EnumDescription("Raport evolutie grupaje")]
        //EvolutieGrupaje = 302,
        //[EnumDescription("Raport evolutie grupaje detaliat")]
        //EvolutieGrupajeDetaliat = 303,
        [EnumDescription("Programari service")]
        ProgramariService = 303,
        [EnumDescription("Raport pe referinta")]
        RaportPeReferinta = 304,
        [EnumDescription("Operatii mecanici")]
        Operatii_Mecanici = 305,
        [EnumDescription("Controlling")]
        Controlling = 306,
        [EnumDescription("Casare")]
        Casare = 307,
        [EnumDescription("Generare plati salarii")]
        GenerarePlatiSalarii = 308,
        [EnumDescription("Calatori selectare soferi")]
        SelectareSoferi = 309,
        [EnumDescription("Firme declaratia 394")]
        FirmeDeclartia394 = 310,
        [EnumDescription("Vizualizare activitate vanzari")]
        Vizualizare_Activitate_Vanzari = 311,
        [EnumDescription("Diagrame angajati")]
        AngajatiDiagrame = 312,
        [EnumDescription("Evidenta devize daune")]
        EvidentaDevizeDaune = 313,
        [EnumDescription("Egalizare Solduri Clienti Furnizori")]
        Egalizare_Solduri_Clienti_Furnizori = 314,
        [EnumDescription("Alimentare Combustibil Subcontractanti")]
        Alimentare_Combustibil_Subcontractanti = 315,
        [EnumDescription("Centralizator cazier")]
        CentralizatorCazier = 316,
        [EnumDescription("Masini Indisponibile")]
        Masini_Indisponibile = 317,
        [EnumDescription("BilantFormulare")]
        BilantFormulare = 318,
        [EnumDescription("Stoc combustibil masini")]
        Stoc_Combustibil_Masini = 319,
        [EnumDescription("Vizualizare stoc combustibil masini")]
        Vizualizare_Stoc_Combustibil_Masini = 320,
        [EnumDescription("Registrul bunurilor de capital")]
        RegistrulBunurilorDeCapital = 321,
        [EnumDescription("Mijloace fixe - generare proces verbal")]
        MF_ProcesVerbal_Generare = 322,
        [EnumDescription("Stare soferi")]
        Stare_soferi = 323,
        [EnumDescription("Diurne soferi")]
        Vizualizare_Diurne_Soferi = 324,
        [EnumDescription("Import telefoane")]
        ImportTelefoane = 325,
        [EnumDescription("Vizualizare telefoane")]
        VizualizareTelefoane = 326,
        [EnumDescription("Note contabile multiple")]
        Note_Contabile_Multiple = 327,
        [EnumDescription("Masini proprii rest rezervor lunar")]
        Masini_Proprii_Rest_Rezervor_Lunar = 328,
        [EnumDescription("Actualizare produse stoc combustibil")]
        ActualizareProduseStocCombustibil = 329,
        [EnumDescription("Vizualizare Import combustibil card facturi")]
        VizualizareImportCombustibilCardFacturi = 330,
        [EnumDescription("Editare repartizare costuri")]
        EditareRepartizareCosturi = 331,
        [EnumDescription("Verificare_Bilant")]
        VerificareBilant = 332,
        [EnumDescription("Vizualizare facturi reevaluare")]
        VizualizareFacturiReevaluare = 333,
        [EnumDescription("Export date detaliat")]
        ExportDateDetaliat=334,
        [EnumDescription("Lista miscari mijloace fixe")]
        MiscariMijloaceFixe = 335,
        [EnumDescription("Storno NIR")]
        GestiuneStornoNir = 336,
        //AsociereTelefoane
        [EnumDescription("Asociere telefoane")]
        AsociereTelefoane = 337,
        [EnumDescription("Vizualizare stoc pe intervale")]
        StocPeIntervaleVizualizare = 338,
        [EnumDescription("Rapoarte custom")]
        RapoarteCustom = 339,
        [EnumDescription("Centralizator bonuri fiscale")]
        CentralizatorBonFiscal = 340,
        [EnumDescription("Confirmari solduri clienti furnizori")]
        ConfirmariSolduriClientiFurnizori = 341,
        [EnumDescription("Printare plicuri")]
        PrintarePlicuri = 342,
        [EnumDescription("ValidareDocumenteAtasate")]
        ValidareDocumenteAtasate = 343,
        
        #region TransporturiTrust (doar pentru Dolotrans)
        [EnumDescription("VizualizareVehiculeTrust")]
        VizualizareVehiculeTrust = 344,
        [EnumDescription("ImportVehiculeTrust")]
        ImportVehiculeTrust = 345,
        [EnumDescription("CreareCursaImportTrust")]
        CreareCursaImportTrust = 346,
        [EnumDescription("ReceptionareInDepozitTrust")]
        ReceptionareInDepozitTrust = 347,
        [EnumDescription("CreareCursaDistibutieTrust")]
        CreareCursaDistibutieTrust = 348,
        [EnumDescription("CalculTarifStocTrust")]
        CalculTarifStocTrust = 349,
        #endregion TransporturiTrust (doar pentru Dolotrans)

        [EnumDescription("VizualizareRapoarte")]
        VizualizareRapoarte = 350,
        [EnumDescription("Vizualizare comenzi in lucru")]
        VizualizareComenziInLucru = 351,
        [EnumDescription("Import date")]
        ImportDate_Vizualizare = 352,
        [EnumDescription("Vizualizare comenzi toate")]
        Vizualizare_Comenzi_Toate = 353,
        [EnumDescription("Vizualizare curse cumulat")]
        Vizualizare_Curse_Cumulat = 354,
        [EnumDescription("Profitabilitate curse")]
        Profitabilitate_Curse = 355,
        [EnumDescription("Control financiar cursa")]
        Control_Financiar_Cursa = 356,
        [EnumDescription("Vizualizare facturi emise")]
        Vizualizare_Facturi_Emise = 357,
        [EnumDescription("Preluare alimentari cu numerar in foaie de parcurs")]
        FoaieParcursAlimentariCuNumerar = 358,
        [EnumDescription("Vizualizare transporturi nefacturate")]
        Vizualizare_Transporturi_Nefacturate = 359,
        [EnumDescription("Vizualizare comenzi nevalidate pentru facturare")]
        Vizualizare_Comenzi_Nevalidate_Pentru_Facturare = 360,
        [EnumDescription("Verificare facturare")]
        Verificare_Facturare = 361,
        [EnumDescription("Vizualizare facturi primite")]
        Vizualizare_Facturi_Primite = 362,
        [EnumDescription("Vizualizare foi concediu")]
        Vizualizare_Foi_Concediu = 363,
        [EnumDescription("Vizualizare comenzi predefinite")]
        Vizualizare_Comenzi_Predefinite = 364,
        [EnumDescription("Reevaluare mijloace fixe")]
        ReevaluareMF = 365,
        [EnumDescription("Selectie elemente")]
        ControlSelectieLookup = 366,
        [EnumDescription("Verificare balanta")]
        Verificare_Balanta = 367,
        [EnumDescription("MasterVizualizareNew")]
        MasterVizualizareNew = 368,
        [EnumDescription("Administrare selectie elemente")]
        AdministrareControlSelectieLookup = 369,
        [EnumDescription("Curse actiuni")]
        Curse_Actiuni = 370,
        [EnumDescription("Clasificari planificare")]
        Clasificari_Planificare = 371,
        [EnumDescription("Nomenclator Clasificari Asignare Coloane Planificator")]
        Nomenclator_Clasificari_Asignare_Coloane_Planificator = 372,
        [EnumDescription("Nomenclator culori")]
        NomenclatorCulori = 373,
        [EnumDescription("Facturi Neplatite Export")]
        FacturiNeplatiteExport = 374,
        [EnumDescription("Facturi Neplatite Export Grupate")]
        FacturiNeplatiteExportGrupate = 375,
        [EnumDescription("Curse preluare tronsoane GPS")]
        CursePreluareTronsoaneGPS = 376,
        [EnumDescription("Vizualizare contracte de munca")]
        Angajati_CIM_Vizualizare = 377,
        [EnumDescription("Vizualizare decizii colective salariale")]
        Angajati_DecizieColectivaSalariala_Vizualizare = 378,
        [EnumDescription("Vizualizare cereri de concediu")]
        Angajati_CerereConcediu_Vizualizare = 379,
        [EnumDescription("Introducere tranzactii alimentari combustibil")]
        TranzactiiAlimentariCombustibil = 380,
        [EnumDescription("Curse preavizare depozit")]
        CursePreavizareDepozit = 381,
        [EnumDescription("Actualizare finantari")]
        ActualizareFinantari = 382,
        [EnumDescription("Facturi stornare avans")]
        FacturiStornareAvans = 383,
        [EnumDescription("Vizualizare stoc depozit")]
        Vizualizare_Stoc_Depozit = 384,
        [EnumDescription("Generare facturi depozit")]
        GenerareFacturiDepozit = 385,
        [EnumDescription("Actualizare produse stoc depozit")]
        ActualizareProduseStocDepozit = 386,
        [EnumDescription("Planificator")]
        PlanificatorNew = 387,
        [EnumDescription("Comenzi - editare cantitati incarcate/descarcate")]
        PartideEditareCantitati = 388,
        [EnumDescription("Generare facturi contracte")]
        GenerareFacturiContracte = 389,
        [EnumDescription("Configurare P&L")]
        ConfigurarePL = 390,
        [EnumDescription("Configurari contabilitate")]
        ConfigurariContabilitate = 392,
        [EnumDescription("Credit note")]
        CreditNote = 393,
        [EnumDescription("Management consumuri")]
        Management_Consumuri = 394,
        [EnumDescription("Cheltuieli diverse")]
        Cheltuieli_Diverse = 395,
        [EnumDescription("Alocare venit comenzi splitate")]
        AlocareVenitComenziSplitate = 396,
        [EnumDescription("Facturi Asigurari")]
        Facturi_Asigurari = 397,
        [EnumDescription("ChangePasswordNew")]
        ChangePasswordNew = 398,
        [EnumDescription("Repartizare costuri suplimentare marfa")]
        RepartizareCosturiSuplimentareMarfa = 399,
        [EnumDescription("Trimitere email carausi")]
        TrimitereEmailCarausi = 400,
        [EnumDescription("StatusVehicule")]
        StatusVehicule = 401,
        [EnumDescription("Management import Excel")]
        ManagementImporturiExcel = 402,
        [EnumDescription("Diurne predefinite")]
        DiurnaPredefinita = 403,
        [EnumDescription("Calcul diurne soferi")]
        DiurneSoferiCalcul = 404,
        [EnumDescription("Refacturare combustibil/taxe")]
        Refacturare = 405,
        [EnumDescription("Generare bon transfer combustibil din import pompa")]
        GenerareBonTransferCombustibilPompa = 406,
        [EnumDescription("Generare bon consum combustibil storno")]
        GenerareBonConsumCombustibilStorno = 407,
        [EnumDescription("Repartizare venituri")]
        RepartizareVenituri_New = 408,
        [EnumDescription("Planificator mesagerie")]
        PlanificatorMesagerie = 409,
        [EnumDescription("Curse asociate")]
        CurseAsociate = 410,
        [EnumDescription("Facturi emise Selectare Casierie")]
        FE_SelectareCasierie = 411,
        [EnumDescription("Planificare vizualizare")]
        PlanificareVizualizare = 412,
        [EnumDescription("Angajati masini asociere")]
        AngajatiMasiniAsociere = 413,
        [EnumDescription("Export firme si facturi emise")]
        ExportFirmeSiFacturiEmise = 414,
        [EnumDescription("PlanificatorStatusuriComenzi")]
        PlanificatorStatusuriComenzi = 415,
        [EnumDescription("Retetar")]
        Retetar = 416,
        [EnumDescription("Facturare avize")]
        AvizeFacturare = 417,
        [EnumDescription("Factura proforma popup")]
        FacturaProformaPopup = 418,
        [EnumDescription("Asociere resurse")]
        AsociereResurse = 419,
        [EnumDescription("Creaza cursa din resurse")]
        ResurseCreazaCursa = 420,
        [EnumDescription("Selectie semiremorca")]
        SelectieSemiremorca = 421,
        [EnumDescription("Splitare partida")]
        SplitarePartida = 422,
        [EnumDescription("Import Combustibil Card Fisa Alimentare")]
        ImportCombustibilCard_FisaAlimentare = 423,
        [EnumDescription("Mesagerie")]
        Mesagerie = 424,
        [EnumDescription("Asociere partida parinte")]
        PlanificatorAsocierePartidaParinte = 425,
        [EnumDescription("Asociere useri gestiuni")]
        GestiuniPerUser = 426,
        [EnumDescription("Factoring")]
        Factoring = 427,
        [EnumDescription("Observatii factura")]
        Observatii_Factura = 428,
        [EnumDescription("Vizualizare observatii factura")]
        Vizualizare_Observatii_Factura = 429,
        [EnumDescription("Sabloane Facturi")]
         Nomenclator_Sabloane_Facturi = 430,
        [EnumDescription("Import Comenzi GPS")]
        Import_Comenzi_GPS = 431,
        [EnumDescription("Cartea mare")]
        CarteaMare = 432,
        [EnumDescription("MesagerieVizualizare")]
        MesagerieVizualizare = 433,
        [EnumDescription("ActiuniCurse_AdaugaPauza")]
        ActiuniCurse_AdaugaPauza = 434,
        [EnumDescription("Comenzi Editare PU")]
        PartideEditarePU = 435,
        [EnumDescription("Vizualizare produse actualizate")]
        VizProduseActualizate = 436,
        [EnumDescription("Vizualizare Inventar MF")]
        VizualizareInventarMF = 437,
        [EnumDescription("Evenimente angajati")]
        VizualizareEvenimenteAngajati = 438,
        [EnumDescription("Programari service")]
        VizualizareProgramariService = 439,
        [EnumDescription("Raport istoric pret achizitie")]
        RaportIstoricPretAchizitie = 440,
        [EnumDescription("Vizualizare Facturi Emise - eFactura")]
        VizualizareFEeFactura = 441,
        [EnumDescription("Firme contracte detalii")]
        Firme_Contracte_Detalii = 442,
        [EnumDescription("Compensari deconturi")]
        Compensari_Deconturi = 443,
        [EnumDescription("Planificare soferi")]
        PlanificareSoferiNew = 444,
        //Id-ul unic pt Saft
        [EnumDescription("AnafSAFT Vizualizare")]
        AnafSaft = 10000,
    }

    public enum PopupViewer
    {
        VizualizareNoteContabile,
        VizualizareReevaluareFacturi,
        VizualizareCosturiRepartizate,
        VizualizareFacturiActualizate,
        VizualizareMasiniScadente,
        VizualizareAngajatiScadente,
        VizualizareCECActualizate,
        VizualizareNomenclatoare,
    }

    public enum TipModulNotaContabila
    {
        Amortizare = 1,
        Alte_Note = 2,
        Asigurari = 3,
        Autofacturare = 4,
        Banca = 5,
        Bon_Consum = 6,
        Casa = 7,
        Cheltuieli_In_Avans = 8,
        Compensare = 9,
        DecontCursa = 10,
        DecontPA = 11,
        DecontAlte = 12,
        Descarcare_Gestiune_Bon_Transfer = 13,
        Descarcare_Gestiune_Facturi_Emise = 14,
        Facturi_Emise = 15,
        Facturi_Primite = 16,
        Inchidere_Cheltuieli = 17,
        Inchidere_Venituri = 18,
        Leasing = 19,
        Salarii = 20,
        Inchidere_TVA = 21,
        Aviz_de_insotire = 22,
        AlteOperatiuniJurnaleTVA = 23,
        AlteDocumenteGestiune = 24,
        NIR = 25,
        Rulaj_Precedent = 26,
        CEC = 27,
        Diferenta_Valutara_Incasari = 28,
        Diferenta_Valutara_Plati = 29,
        Alte_Documente_Gestiune = 30,
        Transfer_Avansuri = 31,
    }

    public enum TipFacturare
    {
        Toate = 0,
        Borderou = 1,
        Simpla = 2
    }

    public enum TipSelectieRezultate
    {
        Multipla,
        Simpla
    }

    public enum TipFacturaEmisa : byte
    {
        Operational = 1,
        Service = 2,
        Comert = 3,
        Storno = 4,
        Altefacturi = 5,
        SoldInitial = 6,
        Avize = 7
    }

    public enum TipFacturaPrimita : byte
    {
        Carausi = 1,
        Achizitie = 2,
        AlteFacturi = 3,
        SolduriInitiale = 4,
        Storno = 5,
        Sosite = 6
    }

    public enum TipFacturaReevaluare
    {
        FacturaEmisa = 0,
        FacturaPrimita = 1
    }

    public enum TipPartida : byte
    {
        Nedefinit = 0,
        Proprie = 1,
        Intermediata = 2,
        Externa = 3,
        Intracomunitara = 4
    }

    public enum TipTarif : byte
    {
        Nedefinit = 0,
        Intern = 1,
        Extern = 2,
        Intracomunitar = 3
    }

    public enum WarningConditionType : byte
    {
        None = 0,
        Between = 1,
        Equal = 2,
        Greater = 3,
        GreaterOrEqual = 4,
        Less = 5,
        LessOrEqual = 6,
        NotBetween = 7,
        NotEqual = 8
    }

    public enum WarningColorLevel
    {
        None = 0,
        Level1 = 1,
        Level2 = 2,
    }

    public enum ReportViewerType
    {
        Document,
        MasterChildFilteredView,
        FilteredView,
        General
    }

    public enum TipModulIncasarePlata : byte
    {
        Banca = 1,
        Casa = 2,
        Compensare = 3,
        Nota_Contabila_Detalii = 4,
        Alte_Cheltuieli_Decont = 5,
        Decont_ParcAuto_Cheltuieli = 6,
        Decont_Cursa_Cheltuieli = 7,
        Factura_Storno = 8,
        CEC = 9
    }

    public enum TipConstituireCursa
    {
        Proprie,
        Intermediara
    }

    public enum TipBlocareBalanta : byte
    {//Legatura cu nomenclatorul [Nomenclator_Tip_Documente_Blocare] pe coloanele [ID] si [CodModul]
        Alte_Documente_Gestiune = 1,
        Alte_Oper_Jurnale = 2,
        Aviz_Insotire_Marfa = 3,
        Bon_Consum = 4,
        Bon_Transfer = 5,
        CEC = 6,
        Cheltuieli_Curse = 7,
        Cheltuieli_Terti = 8,
        Deconturi = 9,
        Facturi_Emise = 10,
        Facturi_Primite = 11,
        PV_Compensare = 12,
        Note_Contabile = 13,
        NIR = 14,
        Registru_Casa = 15,
        Registru_Banca = 16,
        Reparatii = 17,
        Curse = 18,
        MF_Obiecte_De_Inventar_Firma = 19,
        Partide = 20,
        Transfer_Avansuri = 21,
    }

    public enum TipLitigiu : byte
    {
        Somatii = 1,
        Insolvente = 2,
        PlangeriContraventionale = 3,
        PlangeriPenale = 4,
        LitigiiMunca = 5,
        ProceseAvocati = 6,
        DeciziiConcediere = 7,
        AccidenteAuto = 8,
        ExecutariSilite = 9,
        Asiguratori = 10,
        Arhiva = 11
    }

    public enum StatusLotMarfa : byte
    {
        Undefined = 0,
        Ofertare = 1,
        Confirmat = 2,
        InGrupaj = 3,
        InDepozitPartenerImport = 4,
        InCamionGrupaj = 5,
        TransferatInAltTerminalPropriu = 6,
        InLivrare = 7,
        Livrat = 8,
        Complet = 9,
        IncarcatLaNava = 10,
        InCursDeSosire = 11,
        DescarcatInPort = 12,
        SositLaDestinatie = 13,
        IncarcatInAvion = 14,
        DescarcatAeroport = 15,
        PreavizatDepozit = 16,
        InDepozitPartenerExport = 17,
        InDepozitPropriu = 18,
        InCamionColectare = 19,
        InCamionDistributie = 20,
        InCamionFTL = 21,
        InCamion = 22,
        InColectareLaPartener = 23,
        Colectat = 24,
        Trash = 25,
        Preavizat = 26
    }

    public enum NomLotMarfa
    {
        UNDEFINED = 0,
        FTL = 1,
        GRUPAJ = 2,
        PARTIAL = 3,
        DOMESTIC = 4,
        AERIAN = 5,
        MARITIM = 6,
        FTLPROJECT = 7
    }

    public enum NomTipLotMarfa
    {
        UNDEFINED = 0,
        IMPORT = 1,
        EXPORT = 2,
        INTERN = 3,
        COLECTARE = 4,
        DISTRIBUTIE = 5,
        DEPOZIT = 6,
        VAMA = 7,
        ALTELE = 8,
        FTLPROJECT = 9,
        HANDLING = 10,
        INTRACOMUNITAR = 11,
    }

    public enum TipReferintaRegistraturaExpeditii
    {
        LotMarfa = 1,
        Cursa = 2,
        Depozit = 3,
        Vama = 4
    }

    public enum ImportExportLotMarfa
    {
        IMPORT = 1,
        EXPORT = 2,
        TOATE = -1
    }

    public enum StatusCurse
    {
        InLucru = 1,
        Descarcat = 2,
        Incheiata = 3,
        Livrat = 4,
    }

    public enum TipDocumentExpeditiiMarfa
    {
        Undefined = 0,
        AvizExpeditieDinDepozit = 1,
        AvizExpeditie = 2,
        CMR = 3,
        ListaIncarcare = 4,
        ListaDescarcare = 5,
        LoadingList = 6,
        WarehouseAviso = 7,
        ReceptieDepozit = 8,
        Contracte = 9,
        UnloadingList = 12,
        TruckManifest = 13,
        AvizInsotireMarfa = 14,
        ComandaCaraus = 15,
        ConfirmareDeLivrare = 16,
        AvizDeReceptie = 17,
        OrdinColectare = 18,
    }

    public enum Moneda
    {
        RON = 1000,
        USD = 3,
        GBP = 5,
        EUR = 6
    }

    public enum ConditiiLivrare
    {
        EXW = 1,
        FCA = 2,
        CPT = 3,
        CIP = 4,
        DAT = 5,
        DAP = 6,
        DDP = 7,
        FAS = 8,
        FOB = 9,
        CFR = 10,
        CIF = 11
    }

    public enum NomenclatorLiniiGrupajMM
    {
        CARGOLINEImp = 4,
        CARGOLINEExp = 5,
        BURGERMANIAExp = 6,
        BURGERMANIAImp = 7,
        SG_GERMANIAImp = 9,
        SG_GERMANIAExp = 10,
        KH_GERMANIAImp = 11,
        KH_GERMANIAExp = 12,
        BRA_ANGLIAImp = 13,
        BRA_ANGLIAExp = 14,
        BAS_ANGLIAImp = 17,
        BAS_ANGLIAExp = 18,
        LYOImp = 20,
        LYOExp = 22,
        PARImp = 23,
        PARExp = 24,
        BCNImp = 25,
        BCNExp = 26,
        IRUImp = 27,
        IRUExp = 28,
        MILImp = 29,
        MILExp = 30,
        HALImp = 34,
        HALExp = 35,
        ELVIS = 40
    }

    public enum FTPAccountType
    {
        Undefined = 0,
        DELAMODE_RHENUS = 1,
        DELAMODE_STAECO = 2,
        DELAMODE_SEVSTA = 3,
        MM_ROMANIA = 4,
        DELAMODE_UK = 5,
        MM_ROMANIA_CLUJ = 6,
        MM_ROMANIA_TIMISOARA = 7,
        DELAMODE_STAECO_ORADEA = 8,
        DELAMODE_SEVSTA_ORADEA = 9,
        WINDSOFT = 10,
    }

    public enum ImportLotMarfaType
    {
        Undefined = 0,
        DELAMODE_PAMBAC = 1,
        DELAMODE_PALLEX = 2,
        DELAMODE_SPEEDY = 3,
        DELAMODE_CHEP = 4
    }

    public enum FTPDirection
    {
        Undefined = 0,
        IMPORT = 1,
        EXPORT = 2
    }

    public enum TipuriTarife
    {
        COLECTARE = 1,
        LIVRARE = 2,
        COLLECTION = 3,
        DELIVERY = 4,
        SHUTTLE = 5,
        LINE_HAUL = 6,
        TERMINAL_TERMINAL = 7,
        COST_STATISTIC = 8,
        MANIPULARE = 9
    }

    public enum TipuriTari
    {
        RO = 1,
        UE = 2,
        NONUE = 3
    }

    public enum WarehouseTreeNodes
    {
        InternIntrariReceptionata = 3,
        InternIntrariInLucru = 4,
        InternIntrariExpediata = 5,
        InternIesiriReceptionata = 7,
        InternIesiriInLucru = 8,
        InternIesiriExpediata = 9,
        InternationalIntrariReceptionata = 12,
        InternationalIntrariInLucru = 13,
        InternationalIntrariExpediata = 14,
        InternationalIesiriReceptionata = 16,
        InternationalIesiriInLucru = 17,
        InternationalIesiriExpediata = 18,
    }

    public enum FilialeMM
    {
        BWLP = 1,
        OTOPENI = 2,
        CLUJ = 3,
        TIMISOARA = 4
    }

    public enum UsersMM
    {
        EDI = 87,
    }

    public enum FilialeDelamode
    {
        BUCURESTI = 1,
        OTOPENI = 2,
        ORADEA = 3,
        TIMISOARA = 5,
        SIBIU = 6,
        CONSTANTA = 7,
        BACAU = 8,
        URZICENI = 9,
        FERRY = 99, // INTRODUS MANUAL PT. FERRY
    }

    public enum BookingType
    {
        Undefined = 0,
        Constituire = 1, // constituire booking nou
        Integrare = 2, // integrare un lot marfa intr-un booking existent
        Stergere = 3, // stergere booking cu totul
        Unbooking = 4, // unbooking un lot marfa din booking-ul in care exista
        Info = 5, // info booking cu loturile aferente
    }

    public enum TipDocumentFinanciar
    {
        Factura = 1,
        NotaInterna = 2,
        PozitieStatistica = 3
    }

    public enum ModTarifareAerianMaritim
    {
        PerUnitate = 1,
        GreutateTaxabila = 2,
        GreutateBruta = 3,
    }

    public enum ContFurnizor
    {
        MM = 11242,
        Delamode = 14449,
    }

    public enum SubcentreDelamode
    {
        Distributie = 4,
    }

    public enum CustomReportType
    {
        AerianAWB = 1,
        AerianMasterAWB = 2,
        FacturiTipizatDunca = 3,
    }

    public enum StatusCodeList
    {
        Sya,
        Ids,
        Cal,
        Syp
    }

    public enum StatusCode
    {
        Warehouse = 17,
        Expediat = 40,
        Livrat = 50
    }

    public enum ChgsCode
    {
        PC = 1,
        PP = 2,
        CP = 3,
        CC = 4
    }

    public enum SCI
    {
        X = 1,
        C = 2,
        T1 = 3,
        T2 = 4
    }

    public enum GestiuneDocumenteIesire
    {
        BonConsum = 1,
        BonTransfer = 2,
        FacturaEmisa = 3,
        AvizInsotire = 4,
        AlteDocumenteGestiune = 5,
        FacturaPrimita = 6
    }

    public enum TipTVACumparariDunca
    {
        AchizitiiTaxabile = 3,
        BunuriServiciiBeneficiarObligatTaxaArt150 = 13,
        BunuriServiciiBeneficiarObligatTaxaArt160 = 14,
        AchizitiiIntracomunitareDeBunuriSiServicii = 15,
        AchizitiiIntracomunitareBunuriSiServiciiScutite = 16,
        AchizitiiIntracomunitareBunuriSiServiciiNeimpozabile = 17,
        AchizitiiBunuriSiServiciiImportScutiteSauNeimpozabile = 18
    }

    public enum NomenclatorStatusuriDepozit
    {
        InAsteptare = 1,
        Receptionat = 2,
        Expediat = 3
    }

    public enum NomenclatorIndicatoriForecast
    {
        Turnover3rdPartyInclCustoms = 1,
        TurnoverIntercoTI = 2,
        TurnoverIntercoMM = 3,
        UninvoicedTurnover = 4,
        TurnoverInclCustoms = 5,
        CustomsDuties3rdParty = 6,
        TotalTurnover = 7,
        COS3rdParty = 8,
        COSIntercoTI = 9,
        COSIntercoMM = 10,
        COSForUninvoicedTurnover = 11,
        COSAccrued = 12,
        TotalCOSOfPurchasedServices = 13,
        GrossProfit = 14,
        TruckWORentLease = 15,
        TruckWOFuel = 16,
        TruckWOInsuranceMaintenance = 17,
        TotalCostOfTruckWO = 18,
        GrossProfit2 = 19,
        WagesAndSalaries = 20,
        SocialInsuranceAndOtherCosts = 21,
        PersonnelCosts = 22,
        PropertyRentLeaseInclAncillaryCosts = 23,
        CarsRentLeaseInclAncillaryCosts = 24,
        Communication = 25,
        InformationTechnology = 26,
        TravelEntertainment = 27,
        InsuranceDamagesSecurity = 28,
        PRAdvertisingStationery = 29,
        AuditLegalConsultancy = 30,
        TaxesNonIncome = 31,
        OtherCosts = 32,
        Depreciation = 33,
        HeadOfficeConsultancyFees = 34,
        NonOperatingIncome = 35,
        NonOperatingCosts = 36,
        BadDebtsLitigation = 37,
        OtherCostsTotal = 38,
        InterestIncome = 39,
        InterestCost = 40,
        ExchangeRateDifference = 41,
        DividendsFromSubsidiariesInvestments = 42,
        FinanceResult = 43,
        TotalCosts = 44,
        ResultB4Allocation = 45,
        AllocationOfAdministrationCosts = 46,
        EBTLocal = 47,
        EBTCentralAdjustment = 48,
        EBT = 49,
        CorporateIncomeTax = 50,
        EAT = 51,
        FTE = 52
    }

    public enum NomenclatorIndicatoriPL_Delamode
    {
        Turnover = 1,
        SalesRoadFreight = 2,
        IncrementalWarehouseRevenue = 3,
        Customs = 4,
        OtherIncome = 5,
        Ferries = 6,
        SalesAirSeaFreight = 7,
        TotalRevenue = 8,
        DirectCosts = 9,
        HaulageCost = 10,
        WarehouseCost = 11,
        FerryCosts = 12,
        OtherDirectCost = 13,
        AirSeaFrieghtCosts = 14,
        IntercompanyCharges = 15,
        TotalDirectCosts = 16,
        GrossProfit = 17,
        ControllableCosts = 18,
        StaffCosts = 19,
        HeatLightPowerCosts = 20,
        CommunicationCosts = 21,
        SiteCosts = 22,
        EquipmentCosts = 23,
        TravelAndEnrtertainingCosts = 24,
        CompanyCarCosts = 25,
        RecruitmentTrainingCosts = 26,
        ControllableDepreciation = 27,
        ComputerIT = 28,
        BadDebts = 29,
        TotalControllableCosts = 30,
        NoncontrollableCosts = 31,
        OfficeRent = 32,
        Rates = 33,
        Advertising = 34,
        Insurance = 35,
        BankCharges = 36,
        HPCharges = 37,
        LegalFees = 38,
        AuditFees = 39,
        ProfitLossOnDisposalOfAssets = 40,
        Depreciation = 41,
        Consultancy = 42,
        RevaluationCurrency = 43,
        OtherExpensesIncome = 44,
        TotalNoncontrollableCosts = 45,
        Interest = 46,
        ProfitTax = 47,
        EBT = 48
    }

    public enum TipDecizieContracte
    {
        Undefined = 0,
        DecizieIncetareArt31 = 1,
        DecizieIncetareArt55 = 2,
        DecizieIncetareArt65 = 3,
        DecizieIncetareArt56 = 4,
        DecizieIncetareArt81 = 5
    }

    public enum ClaseCashFlowDunca
    {
        RegieFirma = 28
    }

    public enum CentreProfitDunca
    {
        RegieFirma = 15
    }

    public enum SubcentreProfitDunca
    {
        RegieFirma = 70
    }

    public enum ListaTipAutoAnvelope
    {
        Tip4X2 = 1,
        Tip6X2 = 2,
        Tip6X4 = 3,
        Tip8X4 = 4
    }

    public enum TipOperaratiuniAnvelope
    {
        AdaugareaAnvelopa = 1,
        InlocuireAnvelopa = 2,
        InspectieAnvelopa = 3,
        RotatieAnvelopa = 4
    }

    public enum ListaCotaTVA
    {
        CotaTVA0 = 1,
        CotaTVA19 = 2,
        CotaTVA9 = 3,
        CotaTVA24 = 4,
        CotaTVA5 = 5
    }

    public enum FurnizoriImport
    {
        Rompetrol = 1,
        Fde = 2,
        Shel = 906,
        SmartDiesel = 4,
        EuroShell = 5,
        Daars = 6,
        Mol = 7,
        ToolColect = 8,
        IDS = 9,
        OMV = 10,
        AlteTipuri = 11,
        EliTrans = 12,
        Axess = 13,
        Telepass = 14,
        As24 = 15,
        EuroWag = 16,
        ToolVerag = 17,
        TSV = 18,
        DKV = 19,
        Asfinag = 20,
        PloseUnion = 21
    }

    public enum MF_Tip_ProcesVerbal
    {
        PVLuareInPrimire = 1,
        PVCasareOI = 2,
        PVCasareMFCuRecuperare = 3,
        PVCasareMFFaraRecuperare = 4
    }

    public enum Nomenclator_Tip_Referat_HR
    {
        ReferatRCAMalus = 1,
        ReferatAvertismentConsum = 2,
        ReferatDepasireViteza = 3,
        ReferatConstatareEvaluarePagube = 4
    }

    public enum Nomenclator_Tip_Tronson
    {
        Intern = 1,
        Extern = 2
    }

    //Doar pentru Dolotrans
    public enum TipVizualizareTrust
    {
        Vehicule = 1,
        Import = 2,
        Distributie = 3,
        Stocare = 4
    }
    //Doar pentru Dolotrans

    public enum Nomenclator_Panouri : byte
    {
        TMS_Comercial = 1,
        TMS_CheltuieliFlota = 2,
        FleetManagement = 3,
        Finantari_Asigurari = 4,
        FacturiEmise = 5,
        FacturiPrimite = 6,
        Gestiune = 7,
        Service = 8,
        Financiar = 9,
        CreditControl = 10,
        MijloaceFixeSiObiecteDeInventar = 11,
        Contabilitate = 12,
        Registratura = 13,
        HR = 14,
        Juridic = 15,
        Manager = 16,
        Administrare = 17,
        TransporturiTrust = 18,
        VanzareVehicule = 19,
    }

    public enum InterfataVizualizareNoua
    {
        VizualizariProcedura = 1,

        #region Service
        Vizualizari_Service_DevizEstimativ = 2,
        Vizualizare_Service_FacturiEmise = 3,
        Vizualizare_Service_DeFacturat = 4,
        Vizualizare_Service_VizualizareComenzi = 5,
        Vizualizare_Service_OperatiiMecanici = 6,
        Vizualizare_Service_ProcesVerbalConstatare = 7,
        Vizualizare_Service_DevizService = 8,
        Vizualizare_Service_MasiniTerti = 9,
        Vizualizare_Service_LicitatiiProduse = 30,
        Vizualizare_Service_RaportEvenimente = 33,
        #endregion

        #region FleetManagement
        Vizualizare_Import_Combustibil_Card = 10,
        Vizualizare_Import_Combustibil_Pompa = 11,
        Vizualizare_Date_Scadente_Masini =34,
        #endregion

        #region FacturiEmise
        VizualizareBonuriFiscale = 22,
        #endregion

        #region FacturiPrimite
        Vizualizare_FacturiPrimite_CosturiRepartizare = 12,
        #endregion

        #region PanouTMSComercial
        VizualizareComenziInLucru = 13,
        VizualizareComenziInLucruToate = 14,
        VizualizareCurseCumulat = 15,
        VizualizareEvenimenteCurse = 16,
        VizualizareRentabilitateProprii = 17,
        VizualizareRentabilitateIntermedieri = 18,
        #endregion

        #region Firme
        VizualizareFirmeContracte = 19,
        #endregion Firme

     

        #region Contabilitate
        VizualizareDeclaratia390 = 20,
        VizualizareDeclaratia394 = 21,
        #endregion

        #region Import date
        VizualizareImportDateEDI = 23,
        VizualizareImportDateEDI2 = 24,
        //VizualizareImportMachetaTreiro = 26,
        //VizualizareImportMachetaRompetrol = 27,
        #endregion Import date

        #region Panou TMS Cheltuieli
        VizualizareCheltuieliDiverse = 25,
        #endregion Panou TMS Cheltuieli

        #region Planificator
        VizualizareStatusVehicule = 26,
        VizualizareVehiculePeTara = 27,

        #endregion Planificator

        #region comercial
        VizualizareSolicitari = 28,
        #endregion

        #region Scadente
        VizualizarePanouScadente = 29,
        #endregion Scadente

        #region Gestiune
        VizualizareFacturareAvize = 31,
        #endregion Gestiune

        Vizualizare_FAZ_Neproductive = 32,
        VizualizareMFListaMiscari = 35,
        VerificareLoguri = 36,

        //next id = 37
    }

    public enum ColoanePlanificator
    {
        CapTractor = 1,
        Semiremorca = 2,
        Firme = 3,
        Soferi = 4,
        CapTractorCod = 5,
        SemiremorcaCod = 6,
        SemiremorcaFel = 7,
        Departament = 8
    }

    public enum NomenclatorCauze
    {
        Altele = 4,
    }

    public enum FirmaCDL
    {
        CDL = 478,
        CDA = 478
    }

    public enum ParametriFiltrare
    {
        Firma_ID = 1,
        Oras_ID = 2,
        PersoaneContact_ID = 3,
        NrInmatriculareFCT_ID = 4,
        ID_MasinaMT = 5,
        Partida_ID = 6,
        Cursa_ID = 7,
        SemiremorcaProprie_ID = 8,
        Adresa_ID = 9,
        Nomenclator_CarduriMasini_ID = 10,
    }

    public enum TipTarifDepozit
    {
        Undefined = 0,
        Depozitare = 1,
        Manipulare = 2,
        Infoliere = 3,
        Reimpaletare = 4
    }

    public enum Nomenclator_Statusuri_Solicitari
    {
        Noua = 1,
        InLucru = 2,
        Ofertata = 3,
        Finalizata = 4,
        Anulata = 5
    }

    public enum TipCerere
    {
        MarfaGata = 1,
        Cotatie = 2
    }

    public enum ProiecteWindSoft
    {
        /* Mapare cu tabela [dbo].[ProiecteWindSoft]. 
           In aceasta tabela se gasesc doar proiectele achizitionate de client si se foloseste pentru separarea licentelor de aplicatie pe proiecte. */
        WindNetERP = 1,
        WindTyre = 2,
        WindSecurity = 3,
        WindTransporturiConta_WTCAccess = 4,
        WindTransporturi_WTAccess = 5,
        WindParking = 6,
        WindPanel = 7,
        WindOps = 8,
        WindRamps = 9,
        WindProiecte = 10,
    }
    public enum Language
    {
        Romana = 1,
        Engleza = 2,
        Germana = 3
    }

    public enum WialonErrors : int
    {
        [EnumDescription("Successful operation (for example for logout it will be success exit)")]
        Succes = 0,
        [EnumDescription("Invalid session")]
        InavlidSession = 1,
        [EnumDescription("Invalid service name")]
        InvalidServiceName = 2,
        [EnumDescription("Invalid result")]
        InvalidResult = 3,
        [EnumDescription("Invalid input")]
        InvalidInput = 4,
        [EnumDescription("Error performing request")]
        ErrorPerformingRequest = 5,
        [EnumDescription("Unknown error")]
        UnknownError = 6,
        [EnumDescription("Access denied")]
        AccesDenied = 7,
        [EnumDescription("Invalid user name or password")]
        InvalidUserOrPass = 8,
        [EnumDescription("Authorization server is unavailable")]
        AuthorizationServerUnvavailable = 9,
        [EnumDescription("Reached limit of concurrent requests")]
        RequestLimit = 10,
        [EnumDescription("No messages for selected interval")]
        NoMessages = 1001,
        [EnumDescription("Item with such unique property already exists or Item cannot be created according to billing restrictions")]
        ItemError = 1002,
        [EnumDescription("Only one request is allowed at the moment")]
        OneRequestLimit = 1003,
        [EnumDescription("Limit of messages has been exceeded")]
        LimitOfMessages = 1004,
        [EnumDescription("Execution time has exceeded the limit")]
        ExecutionTimeout = 1005,
        [EnumDescription("Your IP has changed or session has expired")]
        IPOrSessionExpired = 1011,
        [EnumDescription("Selected user is a creator for some system objects, thus this user cannot be bound to a new account")]
        NewAccountBoundError = 2014,
        [EnumDescription("Sensor deleting is forbidden because of using in another sensor or advanced properties of the unit")]
        SensorDeletingError = 2015
    }
    public enum ModalitatePlataIncasare
    {
        Virament = 1,
        CECBO = 2,
        Numerar = 3
    }

    public enum CodVizualizareImportDate
    {   /* Mapare cu tabela [VizualizareImport] */
        I1,     //Vizualizare implicita, cu afisarea inregistrarilor in gridul de detalii a formei "UI.Operational.ImportDate_Vizualizare_Form.cs"
        E1,     //Vizualizare loturi de marfa EDI, cu afisarea inregistrarilor in forma "WindNet.UI.EDI.ImportEDI_Form.cs"
        ICC1,   //Vizualizare import combustibil card, cu afisarea inregistrarilor in forma "UI.Operational.ImportCombusitibilCard_Form.cs"
        ICCD1,  //Vizualizare import combustibil card, cu afisarea inregistrarilor in forma "UI.Operational.ImportCombustibilCard_Decont_Form.cs"
        ICCF1,  //Vizualizare import combustibil card facturi, cu afisarea inregistrarilor in forma "UI.Operational.ImportCombusitibilCard_Form.cs"
        BGS2,   //Vizualizare automata de tip BGS2 cu InterfataVizualizareNoua, cu afisarea inregistrarilor in forma "UI.GeneralViewForms.MasterVizualizareNewForm.cs"
        IACF1,  //Vizualizare import anexa combustibil facturi, cu afisarea inregistrarilor in forma "UI.Operational.ImportCombusitibilCard_Form.cs"
    }

    public enum CredentialeImportComenziGPS
    {
        UnileverCredentiale
    }

    public enum MesagerieTip
    {
        Primite = 1,
        Trimise = 2,
        Toate = 3,
        Masini = 4
    }
    public enum StatusuriComenzi
    {
        Finalizata = 1,
        Confirmata = 2,
        Neconfirmata = 3,
        InTranzitDescarcare = 4,
        InTranzitIncarcare = 5,
        AmAjunsLaIncarcare = 6,
        InceputIncarcare = 7,
        SfarsitIncarcare = 8,
        AmAjunsLaDescarcare = 9,
        InceputDescarcare = 10,
        SfarsitDescarcare = 11,
    }

    public enum AnafTipVizualizare : int
    {
        Assets = 1,
        Suppliers = 2,
        Customers = 3,
        GeneralLedgerAccounts = 4,
        GeneralLedgerEntries = 5,
        SalesInvoices = 6,
        PurchaseInvoices = 7,
        Payments = 8,
        MovementGoods = 9,
        TaxTable = 10,
        UOMTable = 11,
        AnalysisTable = 12,
        MovementTable = 13,
        Products = 14,
        Owners = 15,
        PhysicalStock = 16
    }
}
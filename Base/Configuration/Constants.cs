using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Base.Configuration
{
    public class Constants
    {
        public enum Clients : int
        {
            Dunca = 1,
            Dianthus = 2,
            Alex = 3,
            Vectra = 4,
            MM = 5,
            CTP = 6,
            VioTrans = 7,
            Delamode = 8,
            Tabita = 9,
            Filadelfia = 10,
            Aigle = 11,
            Eliton = 12,
            Gerom = 13,
            Rhenus = 14,
            Contransport = 15,
            ContransportRO = 16,
            Transmar = 17,
            Cartrans = 18,
            CartransSpedition = 19,
            ChimTotal = 20,
            Marvi = 21,
            RomVega = 22,
            Rozati = 23,
            KSC = 24,
            Practicom = 25,
            ItcInvestment = 26,
            ItcLogistic = 27
        }

        public static Clients client = Clients.Vectra;

        public static int C_TIMER_WAIT_DIALOG_FORM = 1000;
        public static string C_CONCURRENCY_UPDATE_FAILED = "CONCURRENCY_UPDATE_FAILED";

        public static string C_CONCURRENCY_UPDATE_FAILED_RECORD_DELETED = "CONCURRENCY_UPDATE_FAILED_RECORD_DELETED";
        public static string C_THROW_EXCEPTION_FROM_OUTSIDE = "THROW_EXCEPTION_FROM_OUTSIDE";

        public static string C_EXECUTION_CANCELED_ON_CONFIRMATION = "EXECUTION_CANCELED_ON_CONFIRMATION";

        public static int C_COTA_TVA_19 = 19;
        public static double C_COTA_TVA_19_Value = 1.19D;
        public static double C_COTA_TVA_24_Value = 1.24D;
        public static double C_COTA_TVA_9_Value = 1.09D;

        public static string C_MENTIUNI_TEXT_FACTURA = "Neplata la termen atrage penalizari de 0,5% pentru fiecare zi de intarziere. Spezele si comisioanele bancare sunt in sarcina platitorului.";

        public static int C_DATABASE_TIMEOUT = 9000; /* seconds */

        public static string C_DEFAULT_SKIN_NAME = "";
        public static int C_DEFAULT_SALVARE_DUPA_INTRODUCERE = 1;
        public static bool C_DEFAULT_AFISARE_MESAJ_LA_INCHIDERE_FORM = true;
        public static bool C_DEFAULT_PLANIFICATOR_CROSS_DOCKING = false;

        public static string C_MAIL_ADDRESS = "office@windsoft.ro"; // adresa de mail de la care se trimit mesajele in Planificator catre soferi

        public static bool C_AUTOGENERATE_CODURI = true; /* true - daca se genereaza automat codul de transport de genul T_TD_AN_LUNA_0000001  */
        public static bool C_AUTOGENERATE_COD_CURSA = true; /* true - daca se genereaza automat codul de cursa de genul L-filiala-0000001-initialedispecer  */
        public static bool C_USE_FILIALE = true; //true - daca firma ce ruleaza aplicatia are mai multe filiale
        public static int C_LOCAL_IDENT = 1; //local increment pt tabela casa, banca

        public static string C_APPLICATION_FOLDER = new FileInfo(System.Windows.Forms.Application.ExecutablePath).DirectoryName;
        public static string C_APPLICATION_VERSION = Application.ProductVersion;

        public static int C_ID_TVA_0 = 1;
        public static int C_ID_TVA_19 = 2; // 2 este id-ul cotei TVA 19%. Constanta se foloseste la facturile emise/primite pentru setarea default a tipului de TVA folosit pt jurnalele de TVA
        public static int C_ID_TVA_24 = 4; // 2 este id-ul cotei TVA 24%. Constanta se foloseste la facturile emise/primite pentru setarea default a tipului de TVA folosit pt jurnalele de TVA
        public static int C_ID_Tip_TVA_Vanzari_Taxabile = 1; // obligatoriu pentru cota TVA 19 la facturi emise
        public static int C_ID_Tip_TVA_Cumparari_Din_tara_si_import_extracomunitar_nevoile_firmei = 3; // obligatoriu pentru cota TVA 19 la Facturi primite
        public static int C_ID_FLOTA_CARAUS = 4; //4 este pentru centru de profit asociat curselor intermediare
        public static int C_ID_Transport_Gratuit = 4; //4 este pentru tipul de transport gratuit
        public static int C_ID_FacturiEmise_TransportIntern_ContDebit = 706; //ContDebitID - Lista_Servicii_Facturi, denumireArticol = Transport intern
        public static int C_ID_FacturiEmise_TransportIntern_ContCredit = 487; //ContCreditID - Lista_Servicii_Facturi, denumireArticol = Transport intern

        public static string field_Notificare_Planificator = ""; //campul pentru care se va notifica utilizatorul in Planificator

        public static long C_MAXIMUM_ATTATCHMENT_SIZE = 2000000; //Marime maxima ptr fisiere atasate (Daune, Firme, FirmeMarketing, Angajati firma proprie, Masini proprii detalii) - se foloseste numai ptr salvarea in DB

        public static bool C_SAVE_ATTATCHMENTS_IN_DB = false; // false => atasamentele se salveaza pe server

        public static bool C_BLOCK_AFTER_SAVE = true; // pentru documente, sa se blocheze dupa salvare;

        public static string C_ARCHIVED_FIELD_NAME = "Arhivat";

        public static bool FacturiEmise_ConditieFacturare_Curse_RezolvataCursa = false;//cursa este inchisa operational
        public static bool FacturiEmise_ConditieFacturare_CursaConstituita = true;// partida este inclusa intr-o cursa

        public static bool FacturiEmise_Use_Serii = false;

        public static bool FacturiPrimite_ConditieFacturare_Curse_FacturataCursa = true;//cursa este inchisa operational

        public static Color C_BACK_COLOR_FOCUS_CONTROL = Color.Gainsboro; //culoare de fundal pentru un control (derivat DevExpress) care primeste focus
        public static Color C_BACK_COLOR_INVALID_CONTROL = Color.Red; //culoare de fundal pentru un control (derivat DevExpress) a carui sursa de date este nula
        public static Color C_FORE_COLOR_GRID_COLUMN_LOCKED = Color.Gray;
        public static Color C_BACK_COLOR_GRID_ROW_LOCKED = Color.Gray;

        public static Color C_WARNING_COLOR_LEVEL_1 = Color.Orange;
        public static Color C_WARNING_COLOR_LEVEL_2 = Color.DarkOrange;
        public static Color C_WARNING_COLOR_LEVEL_3 = Color.Red;
        public static Color C_WARNING_COLOR_LEVEL_1_GREEN = Color.Blue;

        public static bool C_PREIA_AUTOMAT_CURS_VALUTAR_LOGIN = true;
        public static bool C_PREIA_AUTOMAT_CURS_VALUTAR_DOCUMENTE = true;

        public static string C_TEXT_ACCESS_DENIED_EXCEPTION = "Nu aveti dreptul de acces la aceasta resursa.";

        public static int C_AVERTIZARE_ZILE_DATE_SCADENTE_MASINI = 20;
        public static int C_AVERTIZARE_ZILE_DATE_SCADENTE_ANGAJATI = 20;
        public static bool C_AVERTIZARE_TOATE_INREGISTRARILE = false;

        public static bool C_USE_DATA_DECONT = true; //false --> se foloseste data de cheltuiala
        public static int C_DATA_DECONT = 1; // 1 in cazul in care decontul se calculeaza in baza campului DataDecont din tabela din BD
        public static int C_DATA_CHELTUIALA = 2; // 2 in cazul in care decontul se calculeaza in baza campului DataCheltiuala din tabela din BD

        public const int C_FIRMA_PROPRIE_ID = 478;

        public static string MailParamsFrom = "sorin.slotea@windsoft.ro";
        public static bool UseSmtpSsl = false;
        public static bool WithSmtp = true;
        public static string SmtpDomain = "";
        public static bool UseDefaultCredentials = false;
        public static string MailServerHost = "89.38.209.22";
        public static int MailServerPort = 25;
        public static string MailServerUser = "sorin.slotea@windsoft.ro";
        public static string MailServerPass = "W1nds@ft";

        public static string MailServerHostDelamode = "mail.delamode.ro";
        public static int MailServerPortDelamode = 25;
        public static string MailServerUserDelamode = "windnet@delamode.ro";
        public static string MailServerPassDelamode = "Dlmd123$";
        public static string MailParamsFromDelamode = "windnet@delamode.ro";
        public static string PasswordClient = "W1ndN3tF4F##!";
        public static bool UseSmtpSslDelamode = false;
        public static bool WithSmtpDelamode = true;
        public static bool SaftYN = false;

        #region FTP 

        public static string FTP_MM_ADDRESS_IMPORT = @"ftp://80.156.58.65/4062/Daten/outbound/";//@"ftp://192.168.0.100/test/"; // @"ftp://192.168.0.100/test/"; @"ftp://86.120.178.88/test/";
        public static string FTP_MM_ADDRESS_EXPORT = @"ftp://80.156.58.65/4062/Daten/inbound"; //@"ftp://192.168.0.100/export/";

        public static string FTP_MM_ADDRESS_IMPORT_CLUJ = @"ftp://80.156.58.65/4070/Daten/outbound/";//@"ftp://192.168.0.100/test/"; // @"ftp://192.168.0.100/test/"; @"ftp://86.120.178.88/test/";
        public static string FTP_MM_ADDRESS_EXPORT_CLUJ = @"ftp://80.156.58.65/4070/Daten/inbound/"; //@"ftp://192.168.0.100/export/";

        public static string FTP_MM_ADDRESS_IMPORT_TIMISOARA = @"ftp://80.156.58.65/4063/Daten/outbound/";//@"ftp://192.168.0.100/test/"; // @"ftp://192.168.0.100/test/"; @"ftp://86.120.178.88/test/";
        public static string FTP_MM_ADDRESS_EXPORT_TIMISOARA = @"ftp://80.156.58.65/4063/Daten/inbound/"; //@"ftp://192.168.0.100/export/";

        public static string FTP_MM_USER = @"mmromania";
        public static string FTP_MM_PASS = @"v3hm9ox";

        public static string FTP_DELAMODE_STAECO_ADDRESS_IMPORT = @"ftp://93.63.211.6/STAECO_OUT/BUCAREST/"; // @"ftp://transfer.de.rhenus.com/import/";
        public static string FTP_DELAMODE_STAECO_ADDRESS_IMPORT_ORADEA = @"ftp://93.63.211.6/STAECO_OUT/ORADEA/"; // @"ftp://transfer.de.rhenus.com/import/";

        public static string FTP_DELAMODE_STAECO_ADDRESS_EXPORT = @"ftp://93.63.211.6/STAECO_IN/"; //@"ftp://transfer.de.rhenus.com/export/";

        public static string FTP_DELAMODE_SEVSTA_ADDRESS_IMPORT = @"ftp://93.63.211.6/SEVSTA_OUT/BUCAREST/"; // @"ftp://transfer.de.rhenus.com/import/";
        public static string FTP_DELAMODE_SEVSTA_ADDRESS_IMPORT_ORADEA = @"ftp://93.63.211.6/SEVSTA_OUT/ORADEA/"; // @"ftp://transfer.de.rhenus.com/import/";
        public static string FTP_DELAMODE_SEVSTA_ADDRESS_EXPORT = @"ftp://93.63.211.6/SEVSTA_IN/"; //@"ftp://transfer.de.rhenus.com/export/";

        public static string FTP_DELAMODE_STAECO_SEVSTA_USER = @"delamode";
        public static string FTP_DELAMODE_STAECO_SEVSTA_PASS = @"dela56ty9wE";
        
        public static string FTP_DELAMODE_RHENUS_ADDRESS_IMPORT = @"ftp://transfer.de.rhenus.com/export/";
        public static string FTP_DELAMODE_RHENUS_ADDRESS_EXPORT = @"ftp://transfer.de.rhenus.com/import/";
                
        public static string FTP_DELAMODE_RHENUS_USER = @"delamode";
        public static string FTP_DELAMODE_RHENUS_PASS = @"Ginene21";

        public static string FTP_DELAMODE_UK_ADDRESS_IMPORT = @"ftp://178.239.100.2/outgoing/";
        public static string FTP_DELAMODE_UK_ADDRESS_EXPORT = @"ftp://178.239.100.2/incoming/";

        public static string FTP_DELAMODE_UK_USER = @"DELROM";
        public static string FTP_DELAMODE_UK_PASS = @"D3lr0m!";

        public static string EDI_FILE_LOCATIONS = new FileInfo(System.Windows.Forms.Application.ExecutablePath).DirectoryName + @"\EDI\";
        public static string EDI_FILE_LOCATIONS_MM = @"E:\WindNet\EDINOU\";

        public static bool FTPYN = false;
        public static string FTP_WINDSOFT_ADDRESS = "";
        public static string FTP_WINDSOFT_USER = "";
        public static string FTP_WINDSOFT_PASS = "";
        public static bool FisiereFTP = false;

        #endregion FTP

        public static int LimbaSelectata = 1;

        public static System.Data.DataSet MultilanguageTraducere = null;

    }
}
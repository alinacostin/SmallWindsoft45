using BaseUtils;
namespace Base.Logs
{
    public static class Interpreter
    {
        public static LogUserInterfaceObject Fill(UserInterfaceType _userInterfaceType)
        {
            switch ((int)_userInterfaceType)
            {
                case (int)UserInterfaceType.Transporturi:
                    return new LogUserInterfaceObject()
                    {                        
                        PrimaryKeyName = "Partida_ID",
                        SelectString = "SELECT P.CodPartida, P.DataTurReturPartida, F.FirmaNume AS Client FROM Partide AS P LEFT JOIN Firme AS F ON P.Client_ID = F.Firma_ID",
                        VisibleFieldNames = "CodPartida,DataTurReturPartida,Client",
                        Captions = "Cod comanda,Data comanda,Client"
                    };
                case (int)UserInterfaceType.Curse_Constituire:
                case (int)UserInterfaceType.Curse_Evidente:
                case (int)UserInterfaceType.Calatori_Curse_Constituire:
                case (int)UserInterfaceType.CamionConstituire:
                case (int)UserInterfaceType.CamionConstituireDomestic:
                case (int)UserInterfaceType.CamionConstituire_FTL_Project:
                case (int)UserInterfaceType.CamionConstituireDelamode:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "Cursa_ID",
                        SelectString = "SELECT C.CodCursa, C.DataCalculCursa, (CASE WHEN ISNULL(C.ProprieIntermedCursa,0) = 1 THEN 'Cursa proprie' ELSE 'Cursa intermediata' END) AS TipCursa, F.FirmaNume AS Caraus, FPC.NumePC AS Dispecer FROM Curse AS C LEFT JOIN Firme AS F ON C.CarausCursa_ID = F.Firma_ID LEFT JOIN Firme_Persoane_Contact AS FPC ON C.DispecerCursa_ID = FPC.PersoaneContact_ID",
                        VisibleFieldNames = "CodCursa,DataCalculCursa,TipCursa,Caraus,Dispecer",
                        Captions = "Cod cursa,Data cursa,Tip cursa,Caraus,Dispecer"
                    };

                case (int)UserInterfaceType.Factura_Emisa_Operational:
                case (int)UserInterfaceType.Factura_Emisa_Altele:
                case (int)UserInterfaceType.Factura_Emisa_Storno:
                case (int)UserInterfaceType.Factura_Emisa_Service:
                case (int)UserInterfaceType.Factura_Emisa_Comert:
                case (int)UserInterfaceType.Factura_Emisa_Operational_Expeditii:
                case (int)UserInterfaceType.Factura_Emisa_SoldInitial:             
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "Factura_ID",
                        SelectString = "SELECT FE.NumarFact, FE.DataFact,F2.FirmaNume AS Cumparator FROM Facturi_Emise AS FE LEFT JOIN Firme AS F2 ON FE.CumparatorFact_ID = F2.Firma_ID",
                        VisibleFieldNames = "NumarFact,DataFact,Cumparator",
                        Captions = "Nr. factura,Data,Cumparator"
                    };


                case (int)UserInterfaceType.Factura_Primita_Caraus:
                case (int)UserInterfaceType.Factura_Primita_Altele:
                case (int)UserInterfaceType.Factura_Primita_Sold_Initial:
                case (int)UserInterfaceType.Factura_Primita_Storno:
                case (int)UserInterfaceType.Factura_Primita_Achizitie:
                case (int)UserInterfaceType.Factura_Primita_Sosita:
                case (int)UserInterfaceType.Factura_Primita_Caraus_Expeditii:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "FacturaCaraus_ID",
                        SelectString = "select FC.NumarFCaraus,FC.DataFCaraus,f.FirmaNume as Furnizor, FPC.NumePC as PersoanaIntocmire from Facturi_Carausi FC left join Firme F on FC.FurnizorFCaraus_ID= f.Firma_ID left join Firme_Persoane_Contact FPC on FC.PersoanaIntocmireFCaraus= FPC.PersoaneContact_ID",
                        VisibleFieldNames = "NumarFCaraus,DataFCaraus,Furnizor,PersoanaIntocmire",
                        Captions = "Nr. factura,Data,Furnizor,Persoana intocmire"
                    };

         
                case (int)UserInterfaceType.Angajati_Adeverinta:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "Adeverinta_ID",
                        SelectString = "SELECT FPC.NumePC AS Angajat, AA.Numar AS NrAdeverinta, AA.Data AS DataAdeverinta FROM Angajati_Adeverinta AS AA LEFT JOIN Firme_Persoane_Contact AS FPC ON AA.Angajat_ID = FPC.PersoaneContact_ID",
                        VisibleFieldNames = "Angajat,NrAdeverinta,DataAdeverinta",
                        Captions = "Angajat,Nr. adeverinta,Data"
                    };
                case (int)UserInterfaceType.SomatiiDePlata:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "Dosar_ID",
                        SelectString = "SELECT FPC.NumePC AS Responsabil, JD.DataDeschidere, JD.NrDosarInstanta FROM Juridic_Dosar AS JD LEFT JOIN Firme_Persoane_Contact AS FPC ON JD.ResponsabilDosar_ID = FPC.PersoaneContact_ID",
                        VisibleFieldNames = "Responsabil,DataDeschidere,NrDosarIstanta",
                        Captions = "Resonsabil dosar,Data deschiderii,Nr dosar instanta"
                    };

                case (int)UserInterfaceType.Carnete_TIR:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "CarnetTIR_ID",
                        SelectString = "select CR.Seria,FPC.NumePC, CR.DataCumparare from Carnete_TIR CR left join Firme_Persoane_Contact FPC on CR.PersoanaResponsabila_ID= FPC.PersoaneContact_ID",
                        VisibleFieldNames = "Seria,PersoanResponsabila,DataCumparare",
                        Captions = "Seria,Persoana responsabila,Data cumparare"
                    }; 

                     case (int)UserInterfaceType.Autorizatii:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "Autorizatie_ID",
                        SelectString = "select A.Seria,A.DataCumparare,A.DataEmitere from Autorizatii A",
                        VisibleFieldNames = "Seria,DataCumparare,DataEmitere",
                        Captions = "Seria,Data Cumparare,Data emitere"
                    };

                     case (int)UserInterfaceType.Chitanta:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "Chitanta_ID",
                        SelectString = "select c.NumarChitanta,c.SerieChitanta,c.DataChitanta from Chitanta c",
                        VisibleFieldNames = "NumarChitanta,SerieChitanta,DataChitanta",
                        Captions = "Nr. Chitanta,Serie chitanta,Data"
                    };

                     case (int)UserInterfaceType.Casa:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "RegistruCasa_ID",
                        SelectString = "select rc.DataRegistruCasa,rc.SoldInitialContabil,rc.SoldFinalContabil,lv.Valuta,ff.Filiala  from Registru_Casa rc  left join Lista_Valute lv on rc. Moneda_ID= lv.Valuta_ID left join Filiale ff on rc.Filiala_ID= ff.Filiala_ID",
                        VisibleFieldNames = "DataRegistruCasa,SoldInitialContabil,SoldFinalContabil,Valuta,Filiala",
                        Captions = "Data casa,Sold initial,Sold final,Valuta,Filiala"
                    };

                     case (int)UserInterfaceType.Banca:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "RegistruBanca_ID",
                        SelectString = " select rb.DataRegistruBanca, rb.SoldInitialCont, rb.SoldFinalCont,ff.Filiala  from Registru_Banca rb left join Filiale ff on rb.Filiala_ID=ff.Filiala_ID",
                        VisibleFieldNames = "DataRegistruBanca,SoldInitialCont,SoldFinalCont,Filiala",
                        Captions = "Data,Sold Initial,Sold final,Filiala"
                    };

                     case (int)UserInterfaceType.Dispozitii_Plata_Incasare:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "DispozitieID",
                        SelectString = "select dpi.NrDispozitie, dpi.DataDispozitie, fpc.NumePC as Angajat,lf.Functie from Dispozitii_Plata_Incasare dpi left join Firme_Persoane_Contact fpc on dpi.AngajatID= fpc.PersoaneContact_ID left join Lista_Functii lf on dpi.FunctiaID= lf.Functie_ID",
                        VisibleFieldNames = "NrDispozitie,DataDispozitie,Angajat,Functie",
                        Captions = "Nr. dispozitie,Data,Angajat,Functie"
                    };

                     case (int)UserInterfaceType.AlteOpJurnaleTVA:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "Operatiune_ID",
                        SelectString = "select aoj.NrDoc, aoj.Data, c.CodCursa,fpc.NumePC Persoana from AlteOpJurnaleTVA aoj left join Curse c on aoj.Cursa_ID= c.Cursa_ID left join Firme_Persoane_Contact fpc on aoj.Persoana_ID= fpc.PersoaneContact_ID",
                        VisibleFieldNames = "NrDoc,Data,CodCursa,Persoana",
                        Captions = "Nr. doc,Data,CodCursa,Persoana"
                    };

                     case (int)UserInterfaceType.Decont_Terti:
                     case (int)UserInterfaceType.Decont_Terti_Grid:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "DecontDACA_ID",
                        SelectString = "select dt.NrDocument, dt.DataCheltuiala, dt.NrDecont,dt.DataDecont from  Decont_Terti dt",
                        VisibleFieldNames = "NrDocument,DataCheltuiala,NrDecont,DataDecont",
                        Captions = "Nr. doc,Data cheltuiala,Nr. decont,Data decont"
                    };

                     case (int)UserInterfaceType.CEC:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "CEC_ID",
                        SelectString = "select cc.NrCEC, cc.Data,nii.Categorie, cc.SumaInRON,f.FirmaNume  from CEC cc  left join Nomenclator_Intrare_Iesire_Casa_Banca  nii on cc.IntrareIesire_ID=nii.ID left join Firme f on cc.Partener_ID= f.Firma_ID",
                        VisibleFieldNames = "NrCEC,Data,Categorie,SumaInRON,FirmaNume",
                        Captions = "Nr. CEC,Data,Categorie,Suma in RON,FirmaNume"
                    };

                     case (int)UserInterfaceType.DCG_Dunca:
                     case (int)UserInterfaceType.Decont_Cursa_General:
                     case (int)UserInterfaceType.DCG_Cheltuieli_Numerar:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "DecontCursaGeneral_ID",
                        SelectString = "select dcg.DataDecont, c.CodCursa as Cursa,FPC.NumePC as PersoanaIntocmire,FPC2.NumePC as PersoanaVerificare from Decont_Cursa_General dcg left join Curse c on dcg.Cursa_ID= c.Cursa_ID left join Angajati_Detalii angd on dcg.PersoanaIntocmire_ID= angd.NumeAngajati_ID left join Firme_Persoane_Contact FPC ON angd.NumeAngajati_ID = FPC.PersoaneContact_ID left join Angajati_Detalii angd1 on dcg.PersoanaVerificare_ID= angd1.NumeAngajati_ID left join Firme_Persoane_Contact FPC2 ON angd.NumeAngajati_ID = FPC2.PersoaneContact_ID",
                        VisibleFieldNames = "DataDecont,Cursa,PersoanaIntocmire,PersoanaVerificare",
                        Captions = "Data,Cursa,Persoana Intocmire,Persoana verificare"
                    };

                     case (int)UserInterfaceType.OP:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "DecontCursaGeneral_ID",
                        SelectString ="select OP.NrOP as Numar,OP.Data, f.FirmaNume as Beneficiar from OP  left join Firme f on OP.Beneficiar_ID= f.Firma_ID",
                        VisibleFieldNames = "Numar,Data,Beneficiar",
                        Captions = "Nmar,Data,Beneficiar"
                    };

                     case (int)UserInterfaceType.Inchidere_Avansuri:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "InchidereAvans_ID",
                        SelectString = "select ia.DataAvansului as Data,ia.DataInchidereAvans as DataInchidere, fpc.NumePC as Persoana,ia.NrDecont from Inchidere_Avansuri ia left join Firme_Persoane_Contact fpc on ia.Persoana_ID= fpc.PersoaneContact_ID",
                        VisibleFieldNames = "Data,DataInchidere,Persoana,NrDecont",
                        Captions = "Data,Data Inchidere,Persoana,Nr. decont"
                    };

                     case (int)UserInterfaceType.PV_Compensare:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "PV_ID",
                        SelectString = "select PV.NrPV,PV.DataPV,TPV.Categorie as TipPV,F1.FirmaNume as Partener from  Proces_Verbal_Compensare PV LEFT JOIN FIRME F1 ON F1.Firma_ID=PV.Partener_ID LEFT JOIN Nomenclator_Tip_PV TPV ON TPV.ID=PV.TipPV_ID",
                        VisibleFieldNames = "NrPV,DataPV,TipPV,Partener",
                        Captions = "Nr. PV,Data,Tip PV,Partener"
                    };

                        case (int)UserInterfaceType.Evidenta_Carduri:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "ID",
                        SelectString = "select NCM.SerieCard,TC.TipDocument,MPD.NrAutoMasiniProprii as NrAuto,NCM.Furnizor from Nomenclator_CarduriMasini NCM LEFT JOIN Lista_Tip_Card TC on NCM.TipCardID=TC.TipDoc_ID LEFT JOIN Masini_Proprii_Detalii MPD on NCM.NrAuto_ID=MPD.NrInmatriculareFCT_ID",
                        VisibleFieldNames = "SerieCard,TipDocument,NrAuto,Furnizor",
                        Captions = "Serie,Tip document,Nr. auto,Furnizor"
                    };

                     case (int)UserInterfaceType.Transfer_Avansuri:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "TransferAvans_ID",
                        SelectString = "select  tra.Data, tra.Suma,lv.Valuta,fpc.NumePC as PersoanaTransfer  from Transfer_Avansuri tra left join Lista_Valute lv on tra.Valuta_ID= lv.Valuta_ID left join Firme_Persoane_Contact fpc on tra.PersoanaTransfer_ID= fpc.PersoaneContact_ID",
                        VisibleFieldNames = "Data,Suma,Valuta,PersoanaTransfer",
                        Captions = "Data,Suma,Valuta,Persoana transfer"
                    };

                     case (int)UserInterfaceType.Inchidere_Decont:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "InchidereAvans_ID",
                        SelectString = "select ia.NrDecont,c.CodCursa as Cursa from Inchidere_Avansuri ia  left join Curse c on ia.Cursa_ID= c.Cursa_ID",
                        VisibleFieldNames = "NrDecont,Cursa",
                        Captions = "Nr. decont,Cursa"
                    };

                     case (int)UserInterfaceType.Firme_Detalii:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "Firma_ID",
                        SelectString = "select f.FirmaNume as Nume,ors.Oras,jud.Judet,tr.Tara,f.FirmaCodFiscal,f.FirmaRegistruComert  from Firme f  left join Lista_Tari tr on f.FirmaTara_ID=tr.Tara_ID left join Lista_Orase ors on f.FirmaOras_ID=ors.Oras_ID left join Lista_Judete jud on f.FirmaJudet_ID= jud.Judet_ID",
                        VisibleFieldNames = "Nume,Oras,Judet,Tara,FirmaCodFiscal,FirmaRegistruComert",
                        Captions = "Nume,Oras,Judet,Tara,Cod fiscal,Registru comert"
                    };

                     case (int)UserInterfaceType.Fisa_Angajat:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "NumeAngajati_ID",
                        SelectString = "select angd.CodAngajati,fpc.NumePC as Nume, angd.CNPAngajati as CNP,angd.DataAngajariiAngajati as DataAngajarii from Angajati_Detalii angd left join Firme_Persoane_Contact fpc on angd.NumeAngajati_ID= fpc.PersoaneContact_ID",
                        VisibleFieldNames = "CodAngajati,Nume,CNP,DataAngajarii",
                        Captions = "Cod angajat,Nume,CNP,Data angajarii"
                    };

                     case (int)UserInterfaceType.Masini_Carausi:
                     case (int)UserInterfaceType.Fisa_Tehnica_Masini_Terti:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "ID_MasinaMT",
                        SelectString = "select mt.NumarInmatriculareMT,lmm.MarcaMasina as Marca,ltct.TipCT as Tip, f.FirmaNume as Firma,mtd.NumeCodFCT from Masini_Terti mt  left join Firme f on mt.ID_FirmaTertiMT= f.Firma_ID  left join Lista_Marci_Masini lmm on mt.MarcaMT_ID= lmm.Marca_Masina_ID  left join Lista_Tipuri_CT ltct on mt.TipMT_ID= ltct.TipCT_ID left join Masini_Terti_Detalii mtd on mt.ID_MasinaMT= mtd.NrAutoTerti_ID",
                        VisibleFieldNames = "NumarInmatriculareMT,Marca,Tip,Firma,NumeCodFCT",
                        Captions = "Nr.inmatriculare,Marca,Tip,Firma,Cod FCT"
                    };

                case (int)UserInterfaceType.Fisa_Tehnica_Masini_Proprii:
                case (int)UserInterfaceType.Fisa_Tehnica_Aeronave:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "NrInmatriculareFCT_ID",
                        SelectString = "select mpd.NrAutoMasiniProprii,mpd.NrSasiuFCT as NumarSasiu,ltct.TipCT as Tip,fpc.NumePC as SoferTitular,mpd.NumeCodFCT from Masini_Proprii_Detalii mpd left join Lista_Tipuri_CT ltct on mpd.TipFCT_ID= ltct.TipCT_ID left join Firme_Persoane_Contact fpc on mpd.SoferTitularFCT_ID=fpc.PersoaneContact_ID",
                        VisibleFieldNames = "NrAutoMasiniProprii,NumarSasiu,Tip,SoferTitular,NumeCodFCT",
                        Captions = "Nr.inmatriculare,Nr. sasiu,Tip,Sofer titular,Cod FCT"
                    };

                case (int)UserInterfaceType.Angajati_Concedii:
                case (int)UserInterfaceType.Foi_Concediu_Active:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "AngajatiConcediiID",
                        SelectString = "select fpc.NumePC as Angajat,angc.DataStart,angc.DataEnd,fpc1.NumePC as Responsabil from AngajatiConcedii angc left join Firme_Persoane_Contact fpc on angc.AngajatID= fpc.PersoaneContact_ID left join Firme_Persoane_Contact fpc1 on angc.ResponsabilID=fpc.PersoaneContact_ID",
                        VisibleFieldNames = "Angajat,DataStart,DataEnd,Responsabil",
                        Captions = "Angajat,Data inceput,Data sfarsit,Responsabil"
                    };

                case (int)UserInterfaceType.Daune:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "Dauna_ID",
                        SelectString = "select mpd.NrAutoMasiniProprii,fpc.NumePC as Sofer,dd.Vinovat,dd.DataProducerii,f.FirmaNume as SocietateAsigurari from Daune dd left join Masini_Proprii_Detalii mpd on dd.Masina_ID=mpd.NrInmatriculareFCT_ID left join Firme_Persoane_Contact fpc on dd.Sofer_ID= fpc.PersoaneContact_ID left join Firme f on dd.SocietateDeAsigurari_ID= f.Firma_ID",
                        VisibleFieldNames = "NrAutoMasiniProprii,Sofer,Vinovat,DataProducerii,SocietateAsigurari",
                        Captions = "Nr. inmatriculare,Sofer,Vinovat,Data producerii,Societate asigurari"
                    };

                case (int)UserInterfaceType.Fisa_Invetar:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "Inventar_ID",
                        SelectString = "select mpd.NrAutoMasiniProprii,inv.DataInventar, fpc.NumePC as Predator, fpc1.NumePC as Primitor from Inventar inv  left join Masini_Proprii_Detalii mpd on inv.Masina_ID= mpd.NrInmatriculareFCT_ID left join Firme_Persoane_Contact fpc on inv.Predator_ID= fpc.PersoaneContact_ID left join Firme_Persoane_Contact fpc1 on inv.Primitor_ID= fpc1.PersoaneContact_ID",
                        VisibleFieldNames = "NrAutoMasiniProprii,DataInventar,Predator,Primitor",
                        Captions = "Nr. inmatriculare,Data,Predator,Primitor"
                    };

                case (int)UserInterfaceType.Firme_Adrese_De_Livrare:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "Adresa_ID",
                        SelectString = "select fadrl.Nume as Firma, fadrl.AdresaDeLivrareFirma as AdresaLivrare, lt.Tara, lo.Oras from Firme_Adrese_De_Livrare fadrl left join Lista_Tari lt on fadrl.Tara_ID=lt.Tara_ID LEFT join Lista_Orase lo on fadrl.Oras_ID= lo.Oras_ID",
                        VisibleFieldNames = "Firma,AdresaLivrare,Tara,Oras",
                        Captions = "Firma,Adresa Livrare,Tara,Oras"
                    };

                case (int)UserInterfaceType.Angajati_Cazier:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "",
                        SelectString = "select fpc.NumePC as Angajat, ntc.Categorie, fpc1.NumePC PersoanaIntocmire,ac.Data from Angajati_Cazier ac  left join Firme_Persoane_Contact fpc on ac.Angajat_ID= fpc.PersoaneContact_ID left join Firme_Persoane_Contact fpc1 on ac.PersoanaIntocmire_ID= fpc1.PersoaneContact_ID left join Nomenclator_Tip_Cazier_Angajati ntc on ac.TipCazier_ID= ntc.ID",
                        VisibleFieldNames = "Angajat,Categorie,PersoanaIntocmire,Data",
                        Captions = "Angajat,Tip cazier,Persoana intocmire,Data"
                    };

                case (int)UserInterfaceType.Fisa_Alimentare_Motorina:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "ImportCombustibilID",
                        SelectString = "select icp.NrAuto,icp.Data, icp.Ora, icp.LitriiAlimentati, fpc.NumePC as Sofer from ImportCombustibilPompa icp left join Firme_Persoane_Contact fpc on icp.Sofer_ID= fpc.PersoaneContact_ID",
                        VisibleFieldNames = "NrAuto,Data,Ora,LitriiAlimentati,Sofer",
                        Captions = "Nr. inmatriculare,Data,Ora,Litrii alimentati,Sofer"
                    };

                case (int)UserInterfaceType.Comenzi_Predefinite:
                    return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "DetaliiSOM_ID",
                        SelectString = "select f.FirmaNume as Firma,mds.TraseulNrIncarcareSOM as Traseu, mds.TarifKm, lv.Valuta from Of_Marfa_Detalii_Salvate mds left join Firme f on mds.FirmaSOM_ID= f.Firma_ID left join Lista_Valute lv on mds.ValutaID= lv.Valuta_ID",
                        VisibleFieldNames = "Firma,Traseu,TarifKm,Valuta",
                        Captions = "Firma,Traseu,TarifKm,Valuta"
                    };

          
                case (int)UserInterfaceType.Asigurari:
                   return new LogUserInterfaceObject()
                    {
                        PrimaryKeyName = "Asigurare_ID",
                        SelectString = "select asig.NrContract, ltasig.TipAsigurare,f.FirmaNume as Asigurator, fpc.NumePC as PersoanaIntocmire from Asigurari asig  left join Lista_Tipuri_Asigurari ltasig on asig.TipAsigurare_ID= ltasig.TipAsigurare_ID left join Firme f on asig.Asigurator_ID= f.Firma_ID left join Firme_Persoane_Contact fpc on asig.PersoanaIntocmire_ID= fpc.PersoaneContact_ID",
                        VisibleFieldNames = "NrContract,TipAsigurare,Asigurator,PersoanaIntocmire",
                        Captions = "Nr. contract,Tip Asigurare,Asigurator,Persoana intocmire"
                    };

                case (int)UserInterfaceType.Finantari:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "Finantare_ID",
                       SelectString = "select fin.NrContract,ltipf.Categorie as TipFinantare,f.FirmaNume as Finantator,fin.ValoareTotalaContract as Valoare,lv.Valuta, fpc.NumePC as PersoanaIntocmire from finantari fin left join Nomenclator_Lista_Tipuri_Finantari ltipf on fin.TipFinantare_ID= ltipf.ID left join Firme f on fin.Finantare_ID= f.Firma_ID left join Angajati_Detalii ad on fin.PersoanaIntocmire_ID= ad.NumeAngajati_ID left join Firme_Persoane_Contact fpc on ad.NumeAngajati_ID= fpc.PersoaneContact_ID left join Lista_Valute lv on fin.Valuta_ID= lv.Valuta_ID",
                       VisibleFieldNames = "NrContract,TipFinantare,Finantator,Valoare,Valuta,PersoanaIntocmire",
                       Captions = "Nr. contract,Tip finantare,Finantator,Valoare,Valuta,Persoana intocmire"
                   };

                case (int)UserInterfaceType.Cheltuieli_Parc_Auto:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "Decont_PACheltuieli_ID",
                       SelectString = "select d.NrDocument, d.NrDecont, d.DataDecont,d.SumaCuTVA,lv.Valuta from Decont_ParcAuto_Cheltuieli d left join Lista_Valute lv on d.Moneda_ID= lv.Valuta_ID",
                       VisibleFieldNames = "NrDocument,NrDecont,DataDecont,SumaCuTVA,Valuta",
                       Captions = "Nr. document,Nr.decont,Data decont,Suma cu TVA, Valuta"
                   };


                case (int)UserInterfaceType.Voyage_Report:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "Voyage_ID",
                       SelectString = "select vr.NrVoyageReport,vr.DataZbor,lo.Oras as LocPlecare,lo1.Oras as LocSosire,f.FirmaNume as Client from Voyage_Report vr left join Lista_Orase lo on vr.LocPlecare_ID=lo.Oras_ID left join Lista_Orase lo1 on vr.LocSosire_ID=lo1.Oras_ID left join Firme f on vr.Client_ID= f.Firma_ID",
                       VisibleFieldNames = "NrVoyageReport,DataZbor,LocPlecare,LocSosire,Client",
                       Captions = "Nr. raport voyage,Data zbor,Loc plecare, Loc sosire,Client"
                   };

           
                case (int)UserInterfaceType.Foaie_Parcurs:
                case (int)UserInterfaceType.Foaie_Parcurs_Consum_Tronsoane:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "FoaieParcurs_ID",
                       SelectString = "select fp.NrFoaie,fp.DataFoaie,c.CodCursa,fpc.NumePC as Sofer, mpd.NrAutoMasiniProprii as CapTractor, mpd1.NrAutoMasiniProprii as Semiremorca from Foaie_De_Parcurs fp left join Curse c on fp.Cursa_ID= c.Cursa_ID left join Firme_Persoane_Contact fpc on fp.Sofer_ID= fpc.PersoaneContact_ID left join Masini_Proprii_Detalii mpd on fp.Auto_ID= mpd.NrInmatriculareFCT_ID left join Masini_Proprii_Detalii mpd1 on fp.Semiremorca_ID= mpd1.NrInmatriculareFCT_ID",
                       VisibleFieldNames = "NrFoaie,DataFoaie,CodCursa,Sofer,CapTractor,Semiremorca",
                       Captions = "Nr. foaie,Data,Cod cursa,Sofer,Cap tractor,Semiremorca"
                   };

                case (int)UserInterfaceType.FAZ_Neproductive:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "FAZNeproductive_ID",
                       SelectString = "select fpmn.NrFoaie,fpmn.DataFoie, fpc.NumePC Persoana,mpd.NrAutoMasiniProprii from Foaie_De_Parcurs_Masini_Neproductive fpmn left join Firme_Persoane_Contact fpc on fpmn.Persoana_ID= fpc.PersoaneContact_ID left join Masini_Proprii_Detalii mpd on fpmn.NrAuto_ID= mpd.NrInmatriculareFCT_ID",
                       VisibleFieldNames = "NrFoaie,DataFoie,Persoana,NrAutoMasiniProprii",
                       Captions = "Nr. foaie,Data foaie,Persoana,Masina"
                   };

                case (int)UserInterfaceType.Foaie_Parcurs_Consum_Gps:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "FoaieDeParcursGpsId",
                       SelectString = "select fp.NrFoaie, fpGps.DataGenerare,fpGps.DataStart, fpGps.DataSfarsit,fpGps.KmBordPlecare from FoaieDeParcursGps fpGps left join Foaie_De_Parcurs fp on fpGps.FoaieParcurs_ID= fp.FoaieParcurs_ID",
                       VisibleFieldNames = "NrFoaie,DataGenerare,DataStart,DataSfarsit,KmBordPlecare",
                       Captions = "Nr. foaie,Data generare,Data start,Data sfarsit,Km bord la plecare"
                   };

                case (int)UserInterfaceType.Foaie_Parcurs_Import_Gps:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "FoaieParcursGPS_ID",
                       SelectString = "select fpiGps.NrFoaie, mpd.NrAutoMasiniProprii as Masina, fpiGps.DataPlecare,fpiGps.DataSosire,fpiGps.TotalKmParcursi from Foaie_Parcurs_Import_GPS fpiGps left join Masini_Proprii_Detalii mpd on fpiGps.NrAuto_ID= mpd.NrInmatriculareFCT_ID",
                       VisibleFieldNames = "NrFoaie,Masina,DataPlecare,DataSosire,TotalKmParcursi",
                       Captions = "Nr. foaie,Masina,Data plecarii,Data sosirii,Km parcursi"
                   };

             

                case (int)UserInterfaceType.Foaie_Parcurs_Consum_Tronsoane_Split:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "FoaieParcurs_ID",
                       SelectString = "select fps.NrFoaie, fps.DataFoaie,c.CodCursa,mpd.NrAutoMasiniProprii as Masina from Foaie_De_Parcurs_Split fps left join Curse c on fps.Cursa_ID= c.Cursa_ID left join Masini_Proprii_Detalii mpd on fps.Auto_ID=mpd.NrInmatriculareFCT_ID",
                       VisibleFieldNames = "NrFoaie,DataFoaie,CodCursa,Masina",
                       Captions = "Nr. foaie,Data,Cod cursa,Masina"
                   };


                case (int)UserInterfaceType.Foaie_Parcurs_Consum_Tronsoane_Gps:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "FoaieParcursViotrans_ID",
                       SelectString = "select fpv.NrFoaie,fpv.DataFoaie,c.CodCursa, mpd.NrAutoMasiniProprii as Masina from Foaie_De_Parcurs_Viotrans fpv left join Curse c on fpv.Cursa_ID= c.Cursa_ID left join Masini_Proprii_Detalii mpd on fpv.Auto_ID= mpd.NrInmatriculareFCT_ID",
                       VisibleFieldNames = "NrFoaie,DataFoaie,CodCursa,Masina",
                       Captions = "Nr. foaie,Data,Cod cursa,Masina"
                   };

              
                case (int)UserInterfaceType.Lista_Produse:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "Produs_ID",
                       SelectString = "select lp.DenumireProdus,lp.CodProdus,ngp.Categorie as Grupa,f.FirmaNume as Furnizor from Lista_Produse lp left join Nomenclator_Grupe_Produse ngp on lp.Grupa_ID= ngp.ID left join Firme f on lp.Furnizor_ID= f.Firma_ID",
                       VisibleFieldNames = "DenumireProdus,CodProdus,Grupa,Furnizor",
                       Captions = "Denumire produs,Cod produs,Grupa,Furnizor"
                   };


                case (int)UserInterfaceType.NIR:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "NIR_ID",
                       SelectString = "select NumarNIR,Data,fc.NumarFCaraus as Factura,f.FirmaNume as Furnizor from NIR left join Facturi_Carausi fc on NIR.FacturaAchizitie_ID=fc.FacturaCaraus_ID left join Firme f on NIR.Furnizor_ID= f.Firma_ID",
                       VisibleFieldNames = "NumarNIR,Data,Factura,Furnizor",
                       Captions = "NIR,Data,Factura,Furnizor"
                   };

                case (int)UserInterfaceType.Aviz_Insotire_Marfa:
                case (int) UserInterfaceType.Aviz_Insotire_Marfa_Iesire:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "Aviz_ID",
                       SelectString = "select aim.NumarAviz,aim.SerieAviz,aim.Data,cl.NumarComanda as Comanda,f.FirmaNume as Furnizor, f1.FirmaNume as Cumparator from Aviz_Insotire_Marfa aim  left join Firme f on aim.Furnizor_ID= f.Firma_ID left join Firme f1 on aim.Cumparator_ID= f1.Firma_ID left join Comanda_Client cl on aim.ComandaClient_ID= cl.ComandaClient_ID",
                       VisibleFieldNames = "NumarAviz,SerieAviz,Data,Comanda,Furnizor,Cumparator",
                       Captions = "Nr. aviz, Serie,Data,Comanda,Furnizor,Cumparator"
                   };

               
                case (int)UserInterfaceType.Bon_De_Consum:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "BonCons_ID",
                       SelectString = "select bc.NrBon, bc.Data, fpc.NumePC as Gestoinar   from Bon_De_Consum bc left join Firme_Persoane_Contact fpc on bc.Gestionar_ID= fpc.PersoaneContact_ID",
                       VisibleFieldNames = "NrBon,Data,Gestoinar",
                       Captions = "Nr. bon,Data,Gestionar"
                   };

                case (int)UserInterfaceType.Bon_Transfer:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "Bon_ID",
                       SelectString = "select bt.NumarBon,bt.Data,cl.NumarComanda as Comanda from Bon_Transfer bt left join Comanda_Client cl on bt.ComandaClient_ID=cl.ComandaClient_ID  ",
                       VisibleFieldNames = "NumarBon,Data,Comanda",
                       Captions = "Nr. bon,Data,Comanda"
                   };

                case (int)UserInterfaceType.Alte_Documente_Gestiune:
                case (int)UserInterfaceType.Alte_Documente_Gestiune_Intrare:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "Document_ID",
                       SelectString = "select adg.NrDocument,adg.Data,ndg.Categorie Tip,fpc.NumePC as Gestionar from Alte_Documente_Gestiune adg left join Nomenclator_Documente_Gestiune ndg on adg.TipDoc_ID= ndg.ID left join Firme_Persoane_Contact fpc on adg.Gestionar_ID=fpc.PersoaneContact_ID",
                       VisibleFieldNames = "NrDocument,Data,Tip,Gestionar",
                       Captions = "Nr. document,Data,Tip,Gestionar"
                   };

                case (int)UserInterfaceType.PV_Transfer_Gestiune_Persoane:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "PVTransfer_ID",
                       SelectString = "select tgp.NrDocument,tgp.Data,fpc.NumePC as PersoanaPredare, fpc1.NumePC as PersoanaPreluare from PV_Transfer_Gestiune_Persoane tgp left join Firme_Persoane_Contact fpc on tgp.PersoanaPredare_ID= fpc.PersoaneContact_ID left join Firme_Persoane_Contact fpc1 on tgp.PersoanaPreluare_ID=fpc1.PersoaneContact_ID",
                       VisibleFieldNames = "NrDocument,Data,PersoanaPredare,PersoanaPreluare",
                       Captions = "Nr. document,Data,Persoana predare,Persoana preluare"
                   };

                case (int)UserInterfaceType.Comanda_Client_Service:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "ComandaClient_ID",
                       SelectString = @"select cc.NumarComanda,
                                        cc.DataComanda,
                                        ntcs.Categorie as TipComanda,
                                        (CASE  when (cc.NrInmatriculare_ID IS NULL ) then mt.NumarInmatriculareMT
                                             when (cc.NrInmatriculareTerti_ID IS NULL) then mpd.NrAutoMasiniProprii 
                                             when (cc.NrInmatriculareTerti_ID IS NULL and cc.NrInmatriculare_ID IS NULL) then NULL
                                         end 
                                         )as Masina,
                                        f.FirmaNume as Client
                                        from Comanda_Client cc
                                        left join Nomenclator_Tip_Comanda_Service ntcs on cc.TipComanda_ID= ntcs.ID
                                        left join Masini_Proprii_Detalii mpd on cc.NrInmatriculare_ID=mpd.NrInmatriculareFCT_ID
                                        left join Masini_Terti mt on cc.NrInmatriculareTerti_ID=mt.ID_MasinaMT
                                        left join Firme f on cc.NumeClient_ID= f.Firma_ID",
                       VisibleFieldNames = "NumarComanda,DataComanda,TipComanda,Masina,Client",
                       Captions = "Nr. comanda,Data comanda,Tip comanda,Masina,Client"
                   };

                case (int)UserInterfaceType.Deviz:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "ComandaDecont_ID",
                       SelectString = "select cd.NrComDec,cd.DataComDec,cc.NumarComanda as ComandaClient from Comanda_Decont cd left join Comanda_Client cc on cd.ComandaClient_ID= cc.ComandaClient_ID",
                       VisibleFieldNames = "NrComDec,DataComDec,ComandaClient",
                       Captions = "Nr. comanda decont,Data,Comanda client"
                   };

                case (int)UserInterfaceType.Deviz_Estimativ:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "DevizEstimativ_ID",
                       SelectString = @"select de.NumarDevizEstim,
	                                       de.DataDevizEstim, 
	                                       (CASE when de.DevizPropriu=1 then 'Deziz propriu'
		                                         when de.DevizTerti=1 then 'Deviz terti'
		                                         else null
		                                         end ) as TipDeviz,                                    		
	                                       (CASE  when (de.NrInmatriculare_ID IS NULL ) then mt.NumarInmatriculareMT
                                                  when (de.NrInmatriculareTerti_ID IS NULL) then mpd.NrAutoMasiniProprii 
                                                  when (de.NrInmatriculareTerti_ID IS NULL and de.NrInmatriculare_ID IS NULL) then NULL
                                                  end  )as Masina,
                                            f.FirmaNume as Client
                                    from Deviz_Estimativ de
                                    left join Masini_Proprii_Detalii mpd on de.NrInmatriculare_ID=mpd.NrInmatriculareFCT_ID
                                    left join Masini_Terti mt on de.NrInmatriculareTerti_ID=mt.ID_MasinaMT
                                    left join Firme f on de.NumeClient_ID= f.Firma_ID ",
                       VisibleFieldNames = "NumarDevizEstim,DataDevizEstim,TipDeviz,Masina,Client",
                       Captions = "Nr. deviz estimativ,Data,Tip deviz,Masina,Client"
                   };


                case (int)UserInterfaceType.Licitatii_Produse:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "Licitatie_ID",
                       SelectString = "select lps.Numar,lps.Data,cc.NumarComanda,fpc.NumePC Persoana from Licitatie_Produse_Service lps left join Comanda_Client cc on lps.Comanda_ID= cc.ComandaClient_ID left join Firme_Persoane_Contact fpc on lps.Persoana_ID= fpc.PersoaneContact_ID",
                       VisibleFieldNames = "Numar,Data,NumarComanda,Persoana",
                       Captions = "Nr. licitatir,Data,Comanda,Persoana"
                   };

                case (int)UserInterfaceType.Nota_Comanda_Client:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "NotaComanda_ID",
                       SelectString = "select ncc.NumarComanda,ncc.Data,cc.NumarComanda as ComandaClient,fpc.NumePC as PersoanaIntocmire from Nota_Comanda_Client ncc left join Comanda_Client cc on ncc.ComandaClient_ID=cc.ComandaClient_ID left join Firme_Persoane_Contact fpc on ncc.PersoanaIntocmire_ID= fpc.PersoaneContact_ID",
                       VisibleFieldNames = "NumarComanda,Data,ComandaClient,PersoanaIntocmire",
                       Captions = "Nr. comanda,Data,Comanda client,Persoana intocmire"
                   };

                case (int)UserInterfaceType.Comercial_Tarifare:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "Oferta_ID",
                       SelectString = "select co.NrOferta, co.DataOferta,f.FirmaNume as Client,fpc.NumePC as PersoanaContact from Comercial_Oferte co left join Firme f on co.ClientOferta_ID= f.Firma_ID left join Firme_Persoane_Contact fpc on co.PersoanaContactOferta_ID= fpc.PersoaneContact_ID",
                       VisibleFieldNames = "NrOferta,DataOferta,Client,PersoanaContact",
                       Captions = "Nr. oferta,Data,Client,Persoana de contact"
                   };

                case (int)UserInterfaceType.Note_Contabile:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "Operatiune_ID",
                       SelectString = "select ncd.NrDocumentOperatiune,ncd.DataOperatiune,fpc.NumePC as PersoanaNotaContabila,f.FirmaNume as ClientFurnizorOpeatiune from Nota_Contabila_Detalii ncd left join Firme f on ncd.ClientFurnizorOperatiune_ID= f.Firma_ID left join Firme_Persoane_Contact fpc on ncd.PersoanaNotaContabila_ID=fpc.PersoaneContact_ID",
                       VisibleFieldNames = "NrDocumentOperatiune,DataOperatiune,PersoanaNotaContabila,ClientFurnizorOpeatiune",
                       Captions = "Nr. document,Data,Persoana nota contabila,Client/Furnizaor operatiune"
                   };

                case (int)UserInterfaceType.Mijloace_Fixe:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "MF_ID",
                       SelectString = "select mfi.NumarDeInventar, mfi.Denumire,lg.Denumire as Grupa,mfi.DataAchizitiei,f.FirmaNume as Furnizor from MF_Obiecte_De_Inventar_Firma mfi left join Firme f on mfi.Furnizor_ID= f.Firma_ID left join MF_Lista_Grupe lg on mfi.Grupa_ID= lg.Grupa_ID",
                       VisibleFieldNames = "NumarDeInventar,Denumire,Grupa,DataAchizitiei,Furnizor",
                       Captions = "Nr. de inventar,Denumire,Grupa,Data achizitiei,Furnizor"
                   };

                case (int)UserInterfaceType.Reevaluare:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "ReevaluareID",
                       SelectString = "select Reevaluare.FacturaID,(case when FacturaEmisaYN=1 then 'Da' else 'Nu'end)as FacturaEmisa, Reevaluare.DataReevaluare from Reevaluare",
                       VisibleFieldNames = "FacturaID,FacturaEmisa,DataReevaluare",
                       Captions = "Factura,Factura emisa,Data reevaluare"
                   };

                case (int)UserInterfaceType.Calatori_Foi_Parcurs:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "CalatoriFoaieParcurs_ID",
                       SelectString = "select cfp.NrFoaie,cfp.DataEliberare, p.CodPartida,ntc.Categorie TipCursa, fpc.NumePC as PersoanaEliberare from Calatori_Foaie_Parcurs cfp left join Partide p on cfp.Partida_ID=p.Partida_ID left join Nomenclator_Tip_Cursa ntc on cfp.TipCursa_ID= ntc.ID left join Firme_Persoane_Contact fpc on cfp.PersoanaEliberare_ID= fpc.PersoaneContact_ID ",
                       VisibleFieldNames = "NrFoaie,DataEliberare,CodPartida,TipCursa,,PersoanaEliberare",
                       Captions = "Nr. foaie,Data eliberare,Cod partida,Tip cursa,Persoana eliberare"
                   };

                case (int)UserInterfaceType.Calatori_Transporturi:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "Partida_ID",
                       SelectString = "select p.CodPartida,c.CodCursa,f.FirmaNume as Client,fpc.NumePC as Dispecer from Partide p left join Curse c on p.CursaPartida_ID= c.Cursa_ID left join Firme f on p.Client_ID= f.Firma_ID left join Firme_Persoane_Contact fpc on p.Dispecer_ID= fpc.PersoaneContact_ID",
                       VisibleFieldNames = "CodPartida,CodCursa,Client,Dispecer",
                       Captions = "Cod partida,Cod cursa,Client,Dispecer"
                   };

                case (int)UserInterfaceType.LotMarfa:
                case (int)UserInterfaceType.LotMarfa_Aerian:
                case (int)UserInterfaceType.LotMarfa_Maritim:
                case (int)UserInterfaceType.LotMarfa_Rutier:
                case (int)UserInterfaceType.LotMarfaMaritim_Delamode:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "LotMarfa_ID",
                       SelectString = "select lm.CodMarfa,nlm.Categorie as TipLotMarfa,nim.Categorie as Impachetare,nlg.Linia as LinieGrupaj from Lot_Marfa lm left join Nomenclator_Tip_Lot_Marfa nlm on lm.TipLotMarfa_ID= nlm.ID left join Nomenclator_ImpachetareMarfa nim on lm.TipImpachetare_ID= nim.ID left join Nomenclator_Linii_Grupaj nlg on lm.LinieGrupaj_ID=nlg.ID",
                       VisibleFieldNames = "CodMarfa,TipLotMarfa,Impachetare,LinieGrupaj",
                       Captions = "Cod marfa,Tip lot marfa,Impachetare,Linie grupaj"
                   };

                case (int)UserInterfaceType.Grupaje:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "Grupaj_ID",
                       SelectString = "select gr.NrGrupaj,gr.DataGrupaj,c.CodCursa, nlg.Linia as LinieGrupaj from Grupaje gr left join Curse c on gr.Cursa_ID= c.Cursa_ID left join Nomenclator_Linii_Grupaj nlg on gr.LinieGrupaj_ID=nlg.ID",
                       VisibleFieldNames = "NrGrupaj,DataGrupaj,CodCursa,LinieGrupaj",
                       Captions = "Nr. grupaj,Data grupaj,Cod cursa,Linie grupaj"
                   };

                case (int)UserInterfaceType.Master_Aerian:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "Master_ID",
                       SelectString = "select ms.NrMaster, ms.Data,fpc.NumePC as Dispecer from Master ms left join Firme_Persoane_Contact fpc on ms.Dispecer_ID= fpc.PersoaneContact_ID ",
                       VisibleFieldNames = "NrMaster,Data,Dispecer",
                       Captions = "Nr. master,Data,Dispecer"
                   };

                case (int)UserInterfaceType.Operatiuni_Vamale:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "OperatiuneVamala_ID",
                       SelectString = "select opv.NrDoc,opv.Data,ndv.Categorie as DocumentInsotire, nopv.Categorie as FelTransport from Operatiuni_Vamale opv left join Nomenclator_Documente_Vamale ndv on opv.DocumentInsotire_ID= ndv.ID left join Nomenclator_Operatiuni_Vama nopv on opv.TipOperatiune_ID= nopv.ID",
                       VisibleFieldNames = "NrDoc,Data,DocumentInsotire,FelTransport",
                       Captions = "Nr. document,Data,Document insotire,Fel transport"
                   };

                case (int)UserInterfaceType.Solicitari:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "Solicitare_ID",
                       SelectString = "select s.NrSolicitare,s.DataPrimiriiSolicitarii,f.FirmaNume as Firma,ff.Filiala from Solicitari s left join Firme f on s.Firma_ID= f.Firma_ID left join Filiale ff on s.Filiala_ID=ff.Filiala_ID",
                       VisibleFieldNames = "NrSolicitare,DataPrimiriiSolicitarii,Firma,Filiala",
                       Captions = "Nr. solicitare, Data primirii,Firma,Filiala"
                   };

                case (int)UserInterfaceType.Oferte:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "Oferta_ID",
                       SelectString = "select ofr.NrOferta,ofr.DataOferta, lnm.NaturaMarfa,f.FirmaNume as Firma from Oferte ofr left join Firme f on ofr.Filiala_ID= f.Firma_ID left join Lista_Natura_Marfa lnm on ofr.NaturaMarfa_ID= lnm.NaturaMarfa_ID",
                       VisibleFieldNames = "NrOferta,DataOferta,NaturaMarfa,Firma",
                       Captions = "Nr. oferta,Data,Natura marfa,Firma"
                   };

                case (int)UserInterfaceType.Warehouse_Intrare_MM:
                case (int)UserInterfaceType.Warehouse_Iesire_MM:
                case (int)UserInterfaceType.Warehouse_Introducere:
                case (int)UserInterfaceType.Warehouse_Alegere_MM:
                case (int)UserInterfaceType.Warehouse_Alege_Document_MM:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "Depozit_ID",
                       SelectString = "select dp.Cod,lm.CodMarfa as LotMarfa,dp.DataPreavizare,dp.NrAuto,f.FirmaNume as Transportator from Depozit dp left join Lot_Marfa lm on dp.LotMarfa_ID= lm.LotMarfa_ID left join Firme f on dp.Transportator_ID=f.Firma_ID",
                       VisibleFieldNames = "Cod,LotMarfa,DataPreavizare,NrAuto,Transportator",
                       Captions = "Cod,Lot marfa,Data preavizare,Nr. auto,Transportator"
                   };

                case (int)UserInterfaceType.CMR:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "CMR_ID",
                       SelectString = "select CMR.NrCMR,c.CodCursa,lm.CodMarfa,fal.Nume as Expeditor, fal1.Nume as Destinatar from CMR left join Curse c on CMR.Cursa_ID= c.Cursa_ID left join Lot_Marfa lm on CMR.LotMarfa_ID=lm.LotMarfa_ID left join Firme_Adrese_De_Livrare fal on CMR.Expeditor_ID=fal.Adresa_ID left join Firme_Adrese_De_Livrare fal1 on CMR.Destinatar_ID=fal1.Adresa_ID",
                       VisibleFieldNames = "NrCMR,CodCursa,CodMarfa,Expeditor,Destinatar",
                       Captions = "Nr. CMR,Cod cursa,Cod marfa,Expeditor,Destinatar"
                   };

                case (int)UserInterfaceType.DocumenteExpeditii_Marfa:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "Document_ID",
                       SelectString = "select lmde.NrAviz, nd.Categorie,lm.CodMarfa as LotMarfa,c.CodCursa from Lot_Marfa_Documente_Expeditie_Pe_Marfa lmde left join Nomenclator_Tip_Documente_Expeditii nd on lmde.TipDocument_ID= nd.ID left join Curse c on lmde.Cursa_ID=c.Cursa_ID left join Lot_Marfa lm on lmde.LotMarfa_ID= lm.LotMarfa_ID",
                       VisibleFieldNames = "NrAviz,Categorie,LotMarfa,CodCursa",
                       Captions = "Nr. aviz,Tip aviz,Lot marfa,Cod cursa"
                   };


                case (int)UserInterfaceType.LotMarfaTarifare:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName ="Tarif_ID", 
                       SelectString = "select f.FirmaNume as Partener,lmte.MC,lmte.ML,ntt.Categorie as TipTarif,lv.Valuta from Lot_Marfa_Tarife_Expeditie lmte left join Firme f on lmte.Partener_ID=f.Firma_ID left join Nomenclator_Tipuri_Tarifare ntt on lmte.TipTarif_ID= ntt.ID left join Lista_Valute lv on lmte.Valuta_ID= lv.Valuta",
                       VisibleFieldNames = "Partener,MC,ML,TipTarif,Valuta",
                       Captions = "Partener,MC,ML,Tip tarif,Valuta"
                   };


                case (int)UserInterfaceType.Contracte:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "ContractComanda_ID",
                       SelectString = "select  ctr.NrContract,ctr.DataContract,f.FirmaNume as Client, lm.CodMarfa as LotMarfa,ctr.NrCamion  from Contracte ctr left join Firme f on ctr.Client_ID=f.Firma_ID left join Lot_Marfa lm on ctr.LotMarfa_ID=lm.LotMarfa_ID",
                       VisibleFieldNames = "NrContract,DataContract,Client,LotMarfa,NrCamion",
                       Captions = "Nr. contract,Data,Client,Lot marfa,Nr. camion"
                   };

                case (int)UserInterfaceType.Documente_Camion:
                case (int)UserInterfaceType.Comanda_Caraus:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "Document_ID",
                       SelectString = "select lmdc.NrDocument,ntd.Categorie as TipDocument,c.CodCursa,NrAuto from Lot_Marfa_Documente_Camion lmdc left join Nomenclator_Tip_Documente_Expeditii ntd on lmdc.TipDocument_ID=ntd.ID left join Curse c on lmdc.Cursa_ID=c.Cursa_ID ",
                       VisibleFieldNames = "NrDocument,TipDocument,CodCursa,NrAuto",
                       Captions = "Nr. document,Tip document,Cod cursa, Nr. auto"
                   };

                case (int)UserInterfaceType.Utilizator:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "User_ID",
                       SelectString = "select u.Username as Utilizator,g.GroupName as Grup, (case when u.Activ=1 then 'Activ' else 'Inactiv' end) as Stare,fl.Filiala from Users u left join Groups g on u.Group_ID=g.Group_ID left join Filiale fl on u.Filiala_ID=fl.Filiala_ID",
                       VisibleFieldNames = "Utilizator,Grup,Stare,Filiala",
                       Captions = "Utilizator,Grup,Stare,Filiala"
                   };

                case (int)UserInterfaceType.GrupUtilizatori:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "Group_ID",
                       SelectString = "select GroupName as Grup from Groups",
                       VisibleFieldNames = "Grup",
                       Captions = "Grup"
                   };

                case (int)UserInterfaceType.Entitati:
                   return new LogUserInterfaceObject()
                   {
                       PrimaryKeyName = "EntityId",
                       SelectString = "select e.EntityName as Entitate,et.EntityTypeName as Tip from Entities e left join EntityTypes et on e.EntityId=et.EntityTypeId ",
                       VisibleFieldNames = "Entitate,Tip",
                       Captions = "Entitate,Tip"
                   };

                default:
                    return null;
            }
        }
    }
}

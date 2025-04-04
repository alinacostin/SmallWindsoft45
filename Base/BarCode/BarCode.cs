using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base.BaseUtils;
using BaseUtils;
using Base.Configuration;
using System.Data.SqlClient;
using Base.DataBase;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;

namespace Base.BarCode
{
    public class BarCode
    {
        public enum BarCodeType : byte
        {
            Undefined = 0,
            Product = 1,
        }

        //public enum BarCodeStandard : byte
        //{
        //    EAN_13 = 0,
        //    EAN_18,
        //    UPC_A,
        //    UPC_E,
        //    CODE_11,
        //    CODE_39,
        //    CODE_93,
        //    CODE_128
        //}

        string BarCodeResult = string.Empty;

        public string GenerateBarCode(Int32 fieldID, BarCodeType type)
        {

            if (type == BarCodeType.Product)
                if (GenerateProductBarCode(fieldID))
                    return BarCodeResult;
            return string.Empty;
        }

        public bool GenerateProductBarCode(Int32 fieldID)
        {
            try
            {
                var barCode = "4";

                barCode += fieldID.ToString().PadLeft(10, '0');
                barCode += CheckDigitUPCACode(barCode);
                BarCodeResult = barCode;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string CheckDigitUPCACode(string barCode)
        {
            int oddSum = 0, evenSum = 0;
            int result = 0;

            for (int i = 0; i <= barCode.Length - 1; i++)
            {
                if (i % 2 == 0)
                    oddSum += UtilsGeneral.ToInteger(barCode[i].ToString(), 0);
                else
                    evenSum += UtilsGeneral.ToInteger(barCode[i].ToString(), 0);
            }
            oddSum = oddSum * 3;
            result = (oddSum + evenSum) % 10;
            result = result == 0 ? 0 : 10 - result;

            return result.ToString();
        }

        public void GenerateAndSaveDocumentCode(UserInterfaceType uType, int fieldID)
        {
            try
            {
                int returnValue = 1;
                while (1 == returnValue)
                {
                    Random r = new Random((int)DateTime.Now.Ticks);
                    var barCode = (fieldID.ToString() + ((int)uType).ToString() + r.Next().ToString()).PadLeft(11, '0');
                    barCode = barCode.Substring(0, 11);
                    barCode += CheckDigitUPCACode(barCode);

                    using (SqlDataReader reader = (SqlDataReader)WindDatabase.ExecuteDataReader("sp_Insert_WindnetDocumentCodes", new object[] { uType != UserInterfaceType.Undefined ? (int)uType : (object)DBNull.Value, fieldID, barCode }))
                    {
                        if (reader.Read())
                        {
                            returnValue = UtilsGeneral.ToInteger(reader["ReturnValue"], 0);
                        }
                        else
                            returnValue = -1;
                    }
                }
            }
            catch { }
        }
    }

    public static class BarCodeImageCreator
    {
        static int Width { get { return 158; } }
        static int Height { get { return 58; } }

        static string StringToEncode { get; set; }
        static string EncodedString { get; set; }
        public enum AlignmentPositions : int { CENTER, LEFT, RIGHT };

        static AlignmentPositions Alignment { get; set; }

        static string[] UPC_CodeA = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
        static string[] UPC_CodeB = { "1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100" };
        static string _Country_Assigning_Manufacturer_Code = "N/A";
        static Hashtable CountryCodes = new Hashtable(); //is initialized by init_CountryCodes()

        public static Image Encode(string _code, AlignmentPositions _alignmentPosition)
        {
            try
            {
                StringToEncode = _code;
                Alignment = _alignmentPosition;
                return Encode();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        static internal Image Encode()
        {
            if (string.IsNullOrEmpty(StringToEncode))
                return null;

            Bitmap b = null;

            b = new Bitmap(Width, Height);
            EncodedString = EncodeUPCA();
            var iBarWidth = Width / EncodedString.Length;

            if (iBarWidth <= 0)
                throw new Exception("Eroare desenare cod bare. Imaginea este prea mare.");

            var shiftAdjustment = (Width % EncodedString.Length) / 2;
            var iBarWidthModifier = 1;

            switch (Alignment)
            {
                case AlignmentPositions.CENTER: shiftAdjustment = (Width % EncodedString.Length) / 2;
                    break;
                case AlignmentPositions.LEFT: shiftAdjustment = 0;
                    break;
                case AlignmentPositions.RIGHT: shiftAdjustment = (Width % EncodedString.Length);
                    break;
                default: shiftAdjustment = (Width % EncodedString.Length) / 2;
                    break;
            }//switch

            //draw image
            int pos = 0;

            using (Graphics g = Graphics.FromImage(b))
            {
                //clears the image and colors the entire background
                //g.Clear(Color.White);

                //lines are fBarWidth wide so draw the appropriate color line vertically
                using (Pen backpen = new Pen(Color.White, iBarWidth / iBarWidthModifier))
                {
                    using (Pen pen = new Pen(Color.Black, iBarWidth / iBarWidthModifier))
                    {
                        while (pos < EncodedString.Length)
                        {

                            if (EncodedString[pos] == '1')
                                g.DrawLine(pen, new Point(pos * iBarWidth + shiftAdjustment + (int)(iBarWidth * 0.5), 0), new Point(pos * iBarWidth + shiftAdjustment + (int)(iBarWidth * 0.5), Height));

                            pos++;
                        }//while
                    }//using
                }//using
            }//using

            DrawLabel(b);
            return b;
        }

        private static Image DrawLabel(Image img)
        {
            try
            {
                System.Drawing.Font font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);

                using (Graphics g = Graphics.FromImage(img))
                {
                    g.DrawImage(img, (float)0, (float)0);

                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;

                    StringFormat f = new StringFormat();
                    f.Alignment = StringAlignment.Near;
                    f.LineAlignment = StringAlignment.Near;
                    int LabelX = 0;
                    int LabelY = 0;

                    LabelX = img.Width / 2;
                    LabelY = img.Height - (font.Height);
                    f.Alignment = StringAlignment.Center;

                    //color a background color box at the bottom of the barcode to hold the string of data
                    g.FillRectangle(new SolidBrush(Color.White), new RectangleF((float)0, (float)LabelY, (float)img.Width, (float)font.Height));

                    //draw datastring under the barcode image
                    g.DrawString(StringToEncode, font, new SolidBrush(Color.Black), new RectangleF((float)0, (float)LabelY, (float)img.Width, (float)font.Height), f);

                    g.Save();
                }//using
                return img;
            }//try
            catch (Exception ex)
            {
                throw new Exception("Eroare generare label: " + ex.Message);
            }//catch
        }//GenerateLabel

        private static string EncodeUPCA()
        {
            //check length of input
            if (StringToEncode.Length != 11 && StringToEncode.Length != 12)
                throw new Exception("Lungimea codului este invalida!");

            if (!CheckNumericOnly(StringToEncode))
                throw new Exception("Codul trebuie sa contina doar numere");

            CheckDigit();

            string result = "101"; //start with guard bars

            //first number
            result += UPC_CodeA[Int32.Parse(StringToEncode[0].ToString())];

            //second (group) of numbers
            int pos = 0;
            while (pos < 5)
            {
                result += UPC_CodeA[Int32.Parse(StringToEncode[pos + 1].ToString())];
                pos++;
            }//while

            //add divider bars
            result += "01010";

            //third (group) of numbers
            pos = 0;
            while (pos < 5)
            {
                result += UPC_CodeB[Int32.Parse(StringToEncode[(pos++) + 6].ToString())];
            }//while

            //forth
            result += UPC_CodeB[Int32.Parse(StringToEncode[StringToEncode.Length - 1].ToString())];

            //add ending guard bars
            result += "101";

            //get the manufacturer assigning country
            InitCountryCodes();
            string twodigitCode = "0" + StringToEncode.Substring(0, 1);
            try
            {
                _Country_Assigning_Manufacturer_Code = CountryCodes[twodigitCode].ToString();
            }//try
            catch
            {
                throw new Exception("Nu a fost identificat codul de tara.");
            }//catch
            finally { CountryCodes.Clear(); }

            return result;
        }//Encode_UPCA

        private static void CheckDigit()
        {
            try
            {
                string RawDataHolder = StringToEncode.Substring(0, 11);

                //calculate check digit
                int even = 0;
                int odd = 0;

                for (int i = 0; i < RawDataHolder.Length; i++)
                {
                    if (i % 2 == 0)
                        odd += Int32.Parse(RawDataHolder.Substring(i, 1)) * 3;
                    else
                        even += Int32.Parse(RawDataHolder.Substring(i, 1));
                }//for

                int total = even + odd;
                int cs = total % 10;
                cs = 10 - cs;
                if (cs == 10)
                    cs = 0;

                StringToEncode = RawDataHolder + cs.ToString()[0];
            }//try
            catch
            {
                throw new Exception("Eroare la calcularea checkdigit.");
            }//catch
        }//CheckDigit

        internal static bool CheckNumericOnly(string Data)
        {
            //This function takes a string of data and breaks it into parts and trys to do Int64.TryParse
            //This will verify that only numeric data is contained in the string passed in.  The complexity below
            //was done to ensure that the minimum number of interations and checks could be performed.

            //9223372036854775808 is the largest number a 64bit number(signed) can hold so ... make sure its less than that by one place
            int STRING_LENGTHS = 18;

            string temp = Data;
            string[] strings = new string[(Data.Length / STRING_LENGTHS) + ((Data.Length % STRING_LENGTHS == 0) ? 0 : 1)];

            int i = 0;
            while (i < strings.Length)
                if (temp.Length >= STRING_LENGTHS)
                {
                    strings[i++] = temp.Substring(0, STRING_LENGTHS);
                    temp = temp.Substring(STRING_LENGTHS);
                }//if
                else
                    strings[i++] = temp.Substring(0);

            foreach (string s in strings)
            {
                long value = 0;
                if (!Int64.TryParse(s, out value))
                    return false;
            }//foreach

            return true;
        }//CheckNumericOnly

        static void InitCountryCodes()
        {
            CountryCodes.Clear();
            CountryCodes.Add("00", "US / CANADA");
            CountryCodes.Add("01", "US / CANADA");
            CountryCodes.Add("02", "US / CANADA");
            CountryCodes.Add("03", "US / CANADA");
            CountryCodes.Add("04", "US / CANADA");
            CountryCodes.Add("05", "US / CANADA");
            CountryCodes.Add("06", "US / CANADA");
            CountryCodes.Add("07", "US / CANADA");
            CountryCodes.Add("08", "US / CANADA");
            CountryCodes.Add("09", "US / CANADA");
            CountryCodes.Add("10", "US / CANADA");
            CountryCodes.Add("11", "US / CANADA");
            CountryCodes.Add("12", "US / CANADA");
            CountryCodes.Add("13", "US / CANADA");

            CountryCodes.Add("20", "IN STORE");
            CountryCodes.Add("21", "IN STORE");
            CountryCodes.Add("22", "IN STORE");
            CountryCodes.Add("23", "IN STORE");
            CountryCodes.Add("24", "IN STORE");
            CountryCodes.Add("25", "IN STORE");
            CountryCodes.Add("26", "IN STORE");
            CountryCodes.Add("27", "IN STORE");
            CountryCodes.Add("28", "IN STORE");
            CountryCodes.Add("29", "IN STORE");

            CountryCodes.Add("30", "FRANCE");
            CountryCodes.Add("31", "FRANCE");
            CountryCodes.Add("32", "FRANCE");
            CountryCodes.Add("33", "FRANCE");
            CountryCodes.Add("34", "FRANCE");
            CountryCodes.Add("35", "FRANCE");
            CountryCodes.Add("36", "FRANCE");
            CountryCodes.Add("37", "FRANCE");

            CountryCodes.Add("40", "GERMANY");
            CountryCodes.Add("41", "GERMANY");
            CountryCodes.Add("42", "GERMANY");
            CountryCodes.Add("43", "GERMANY");
            CountryCodes.Add("44", "GERMANY");

            CountryCodes.Add("45", "JAPAN");
            CountryCodes.Add("46", "RUSSIAN FEDERATION");
            CountryCodes.Add("49", "JAPAN (JAN-13)");

            CountryCodes.Add("50", "UNITED KINGDOM");
            CountryCodes.Add("54", "BELGIUM / LUXEMBOURG");
            CountryCodes.Add("57", "DENMARK");

            CountryCodes.Add("64", "FINLAND");

            CountryCodes.Add("70", "NORWAY");
            CountryCodes.Add("73", "SWEDEN");
            CountryCodes.Add("76", "SWITZERLAND");

            CountryCodes.Add("80", "ITALY");
            CountryCodes.Add("81", "ITALY");
            CountryCodes.Add("82", "ITALY");
            CountryCodes.Add("83", "ITALY");
            CountryCodes.Add("84", "SPAIN");
            CountryCodes.Add("87", "NETHERLANDS");

            CountryCodes.Add("90", "AUSTRIA");
            CountryCodes.Add("91", "AUSTRIA");
            CountryCodes.Add("93", "AUSTRALIA");
            CountryCodes.Add("94", "NEW ZEALAND");
            CountryCodes.Add("99", "COUPONS");

            CountryCodes.Add("471", "TAIWAN");
            CountryCodes.Add("474", "ESTONIA");
            CountryCodes.Add("475", "LATVIA");
            CountryCodes.Add("477", "LITHUANIA");
            CountryCodes.Add("479", "SRI LANKA");
            CountryCodes.Add("480", "PHILIPPINES");
            CountryCodes.Add("482", "UKRAINE");
            CountryCodes.Add("484", "MOLDOVA");
            CountryCodes.Add("485", "ARMENIA");
            CountryCodes.Add("486", "GEORGIA");
            CountryCodes.Add("487", "KAZAKHSTAN");
            CountryCodes.Add("489", "HONG KONG");

            CountryCodes.Add("520", "GREECE");
            CountryCodes.Add("528", "LEBANON");
            CountryCodes.Add("529", "CYPRUS");
            CountryCodes.Add("531", "MACEDONIA");
            CountryCodes.Add("535", "MALTA");
            CountryCodes.Add("539", "IRELAND");
            CountryCodes.Add("560", "PORTUGAL");
            CountryCodes.Add("569", "ICELAND");
            CountryCodes.Add("590", "POLAND");
            CountryCodes.Add("594", "ROMANIA");
            CountryCodes.Add("599", "HUNGARY");

            CountryCodes.Add("600", "SOUTH AFRICA");
            CountryCodes.Add("601", "SOUTH AFRICA");
            CountryCodes.Add("609", "MAURITIUS");
            CountryCodes.Add("611", "MOROCCO");
            CountryCodes.Add("613", "ALGERIA");
            CountryCodes.Add("619", "TUNISIA");
            CountryCodes.Add("622", "EGYPT");
            CountryCodes.Add("625", "JORDAN");
            CountryCodes.Add("626", "IRAN");
            CountryCodes.Add("690", "CHINA");
            CountryCodes.Add("691", "CHINA");
            CountryCodes.Add("692", "CHINA");

            CountryCodes.Add("729", "ISRAEL");
            CountryCodes.Add("740", "GUATEMALA");
            CountryCodes.Add("741", "EL SALVADOR");
            CountryCodes.Add("742", "HONDURAS");
            CountryCodes.Add("743", "NICARAGUA");
            CountryCodes.Add("744", "COSTA RICA");
            CountryCodes.Add("746", "DOMINICAN REPUBLIC");
            CountryCodes.Add("750", "MEXICO");
            CountryCodes.Add("759", "VENEZUELA");
            CountryCodes.Add("770", "COLOMBIA");
            CountryCodes.Add("773", "URUGUAY");
            CountryCodes.Add("775", "PERU");
            CountryCodes.Add("777", "BOLIVIA");
            CountryCodes.Add("779", "ARGENTINA");
            CountryCodes.Add("780", "CHILE");
            CountryCodes.Add("784", "PARAGUAY");
            CountryCodes.Add("785", "PERU");
            CountryCodes.Add("786", "ECUADOR");
            CountryCodes.Add("789", "BRAZIL");

            CountryCodes.Add("850", "CUBA");
            CountryCodes.Add("858", "SLOVAKIA");
            CountryCodes.Add("859", "CZECH REPUBLIC");
            CountryCodes.Add("860", "YUGLOSLAVIA");
            CountryCodes.Add("869", "TURKEY");
            CountryCodes.Add("880", "SOUTH KOREA");
            CountryCodes.Add("885", "THAILAND");
            CountryCodes.Add("888", "SINGAPORE");
            CountryCodes.Add("890", "INDIA");
            CountryCodes.Add("893", "VIETNAM");
            CountryCodes.Add("899", "INDONESIA");

            CountryCodes.Add("955", "MALAYSIA");
            CountryCodes.Add("977", "INTERNATIONAL STANDARD SERIAL NUMBER FOR PERIODICALS (ISSN)");
            CountryCodes.Add("978", "INTERNATIONAL STANDARD BOOK NUMBERING (ISBN)");
            CountryCodes.Add("979", "INTERNATIONAL STANDARD MUSIC NUMBER (ISMN)");
            CountryCodes.Add("980", "REFUND RECEIPTS");
            CountryCodes.Add("981", "COMMON CURRENCY COUPONS");
            CountryCodes.Add("982", "COMMON CURRENCY COUPONS");
        }//init_CountryCodes
    }

    public static class DrawBarCodeImage
    {
        private static Bitmap newBitmap;
        private static Graphics g;
        private static int barCodeHeight = 80;
        private static int placeMarker = 0;
        private static int imageWidth = 0;
        private static float imageScale = 1;
        private static string UPCABegin = "0000000000000101";
        private static string[] UPCALeft = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
        private static string UPCAMiddle = "01010";
        private static string[] UPCARight = { "1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100" };
        private static string UPCAEnd = "1010000000000000";

        public static Image CreateBarCodeImage(string txt, int scale)
        {
            Image img = null;
            imageWidth = 160;
            imageScale = scale;
            imageWidth = System.Convert.ToInt32(imageWidth * imageScale);
            int imageHeightHolder = System.Convert.ToInt32(barCodeHeight * imageScale);
            string incomingString = txt.ToUpper();
            if (incomingString.Length == 0)
            {
                return img;
            }
            int numberOfChars = incomingString.Length;
            newBitmap = new Bitmap((imageWidth), imageHeightHolder, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            g = Graphics.FromImage(newBitmap);
            g.ScaleTransform(imageScale, imageScale);
            Rectangle newRec = new Rectangle(0, 0, (imageWidth), imageHeightHolder);
            g.FillRectangle(new SolidBrush(Color.White), newRec);
            placeMarker = 0;
            txt = txt.Substring(0, 11) + GetCheckSum(txt).ToString();
            int wholeSet = 0;
            for (wholeSet = 1; wholeSet <= System.Convert.ToInt32(incomingString.Length); wholeSet++)
            {
                int currentASCII = (int)Convert.ToChar((incomingString.Substring(wholeSet - 1, 1))) - 48;
                string currentLetter = "";
                if (currentLetter == "")
                    currentLetter = "";
                if (wholeSet == 1)
                {
                    DrawSet(UPCABegin, placeMarker, 0);
                    DrawSet(UPCALeft[currentASCII], placeMarker, 0);
                }
                else if (wholeSet <= 5)
                {
                    DrawSet(UPCALeft[currentASCII], placeMarker, 6);
                }
                else if (wholeSet == 6)
                {
                    DrawSet(UPCALeft[currentASCII], placeMarker, 6);
                    DrawSet(UPCAMiddle, placeMarker, 0);
                }
                else if (wholeSet <= 11)
                {
                    DrawSet(UPCARight[currentASCII], placeMarker, 6);
                }
                else if (wholeSet == 12)
                {
                    DrawSet(UPCARight[currentASCII], placeMarker, 0);
                    DrawSet(UPCAEnd, placeMarker, 0);
                }
            }

            System.Drawing.Font font = new System.Drawing.Font("Times New Roman, Bold", 12.5F);
            try
            {
                g.DrawString(txt.Substring(0, 1), font, Brushes.Black, new System.Drawing.PointF(0, 50));
                g.DrawString(txt.Substring(1, 5), font, Brushes.Black, new System.Drawing.PointF(11, 50));
                g.DrawString(txt.Substring(6, 5), font, Brushes.Black, new System.Drawing.PointF(60, 50));
                g.DrawString(txt.Substring(11, 1), font, Brushes.Black, new System.Drawing.PointF(108, 50));
            }
            finally
            {
                font.Dispose();
            }
            img = Image.FromHbitmap(newBitmap.GetHbitmap());
            return img;
        }

        public static int GetCheckSum(string barCode)
        {
            string leftSideOfBarCode = barCode.Substring(0, 11);
            int total = 0;
            int currentDigit = 0;
            int i = 0;
            for (i = 0; i <= leftSideOfBarCode.Length - 1; i++)
            {
                currentDigit = Convert.ToInt32(leftSideOfBarCode.Substring(i, 1));
                if (((i - 1) % 2 == 0))
                {
                    total += currentDigit * 1;
                }
                else
                {
                    total += currentDigit * 3;
                }
            }
            int iCheckSum = (10 - (total % 10)) % 10;
            return iCheckSum;
        }

        private static void DrawSet(string upcCode, int drawLocation, int barHeight)
        {
            int[] currentLetterArray = new int[upcCode.Length];
            placeMarker += upcCode.Length;
            barHeight = barCodeHeight - barHeight - 15;
            int i = 0;
            for (i = 0; i <= upcCode.Length - 1; i++)
            {
                currentLetterArray[i] = Convert.ToInt16(upcCode.Substring(i, 1));
            }
            for (i = 0; i <= upcCode.Length - 1; i++)
            {
                if (currentLetterArray[i] == 0)
                {
                    g.DrawLine(Pens.White, i + (drawLocation), 0, i + (drawLocation), barHeight - 15);
                }
                else if (currentLetterArray[i] == 1)
                {
                    g.DrawLine(Pens.Black, i + (drawLocation), 0, i + (drawLocation), barHeight - 15);
                }
            }
        }
    }
}

/*
 * AutoIE - Automatisierung vom Internet Explorer
 * ----------------------------------------------
 * Version: 1.0
 * Copyright © 2007 Konstantin Gross
 * http://www.texturenland.de
 *
*/

#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using mshtml;
using SHDocVw;

#endregion Usings

namespace TL.AutoIE
{
    /// <summary>
    /// Diese Klasse erlaubt, den Internet Explorer zu automatisieren
    /// </summary>
    public class ControlIE
    {
        #region Globale Variablen

        /// <summary>
        /// Adresse der URL
        /// </summary>
        private string url;

        /// <summary>
        /// Wurde der IE gestartet?
        /// </summary>
        public bool launched;

        /// <summary>
        /// Wurde die Seite komplett geladen?
        /// </summary>
        private bool finished;

        /// <summary>
        /// Gespeicherte Fehlermeldung
        /// </summary>
        private string errorMsg;

        /// <summary>
        /// Instanz des Internet Explorers
        /// </summary>
        public InternetExplorer iexplore;

        /// <summary>
        /// Geladene Internetseite
        /// </summary>
        private HTMLDocument myDoc;

        /// <summary>
        /// Liste mit den zu übergebenden Werten
        /// </summary>
        private Dictionary<string, string> fField = new Dictionary<string, string>();

        /// <summary>
        /// Werden beim Klick auf den Button, Werte übertragen?
        /// </summary>
        private bool bOnly;

        /// <summary>
        /// Name des zu drückenden Buttons
        /// </summary>
        private string btnName;

        #endregion Globale Variablen

        #region Eigenschaften

        /// <summary>
        /// Erhält oder definiert ob ein Button mit zu übermittelnden Werten gedrückt wird
        /// </summary>
        /// <value>Wert ist ein Boolean Typ</value>
        /// <returns>True, wenn der Button ohne zu übermittelnde Werte gedrückt wird</returns>
        private bool Only
        {
            get
            {
                return bOnly;
            }
            set
            {
                bOnly = value;
            }
        }

        /// <summary>
        /// Erhält oder definiert den Namen des zu drückenden Buttons
        /// </summary>
        /// <value>Wert ist ein String Typ</value>
        /// <returns>Der zu ermittelnde Button, der automatisch gedrückt wird</returns>
        private string ButtonName
        {
            get
            {
                return btnName;
            }
            set
            {
                btnName = value;
            }
        }

        #endregion Eigenschaften

        #region Funktionen

        /// <summary>
        /// Internet Explorer starten
        /// </summary>
        public void ieStart()
        {
            try
            {
                //Wurde der IE noch nicht gestartet?
                if (launched == false)
                {
                    iexplore = new InternetExplorerClass();
                    myDoc = new HTMLDocumentClass();
                    launched = true;
                    iexplore.DocumentComplete += new DWebBrowserEvents2_DocumentCompleteEventHandler(ieDocumentComplete);
                }
                else
                {
                    errorMsg = "Der Internet Explorer ist bereits gestartet.";
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Auf Internetseite surfen
        /// </summary>
        /// <param name="isVisible">Soll das IE-Fenster sichtbar sein?</param>
        /// <param name="Url">Adresse der URL</param>
        public void ieNavigate(bool isVisible, string Url)
        {
            ieStart();
            object o = null;
            iexplore.Navigate(Url, ref o, ref o, ref o, ref o);
            iexplore.Visible = isVisible;
            url = Url;
        }

        /// <summary>
        /// Event das beim laden der Seite ausgelöst wird, wenn sie fertig geladen wurde
        /// </summary>
        private void ieDocumentComplete(object pDisp, ref object URL)
        {
            /* Ist die Seite vollständig geladen?
             * Anmerkung: Diese Abfrage dient dazu eine Endlosschleife zu verhindern, die das IE-Fenster
             * zum Absturz bringt.
             */

            if (finished == false)
            {
                //Soll "nur" der Button gedrückt werden?
                if (Only == true)
                {
                    try
                    {
                        myDoc = new HTMLDocumentClass();
                        myDoc = (HTMLDocument)iexplore.Document;

                        HTMLInputElement btnButton = (HTMLInputElement)myDoc.all.item(this.ButtonName, 0);
                        btnButton.click();
                    }
                    catch (Exception ex)
                    {
                        errorMsg = ex.Message;
                    }
                }
                //HTML-Elemente ausfüllen und Button drücken aber nur wenn ein Button Name angegeben wurde!
                else
                {
                    try
                    {
                        //Durchlaufe alle übergebenen IDs und übergebe die Werte
                        foreach (KeyValuePair<string, string> str in fField)
                        {
                            myDoc = new HTMLDocumentClass();
                            myDoc = (HTMLDocument)iexplore.Document;

                            HTMLInputElement oTextbox = (HTMLInputElement)myDoc.all.item(str.Key, 0);
                            oTextbox.value = str.Value;
                        }
                        //Wurde kein Button Name angegeben, ignoriere den ButtonClick Code
                        if (this.ButtonName != null)
                        {
                            HTMLInputElement btnButton = (HTMLInputElement)myDoc.all.item(this.ButtonName, 0);
                            btnButton.click();
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMsg = ex.Message;
                    }
                }
            }
            finished = true;
        }

        /// <summary>
        /// Das Fenster des Internet Explorers schließen
        /// </summary>
        public void ieClose()
        {
            try
            {
                //Stoppe den IE und schließe ihn
                iexplore.Stop();
                iexplore.Quit();

                launched = false;
                finished = false;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Lädt den HTML-Code von der Seite runter
        /// </summary>
        /// <remarks>Nicht sehr schnell</remarks>
        /// <returns>HTML-Quelltext</returns>
        public string ieGetHTML()
        {
            string Url = url;
            WebClient WC = new WebClient();
            Stream s = WC.OpenRead(url);
            StreamReader sr = new StreamReader(s);
            string htmlSource = sr.ReadToEnd();
            return htmlSource;
        }

        /// <summary>
        /// Wert in ein HTML-Element eintragen
        /// </summary>
        /// <param name="Value">Zu übergebender Wert</param>
        /// <param name="FieldName">Name des Elements</param>
        public void ieFillField(string Value, string FieldName)
        {
            fField.Add(FieldName, Value);
        }

        /// <summary>
        /// Einen Klick auf einen Button ausführen
        /// </summary>
        /// <param name="ButtonName">Name des Buttons</param>
        /// <param name="Only">Werden Werte übermittelt?</param>
        public void ieButtonSubmit(string ButtonName, bool Only)
        {
            this.ButtonName = ButtonName;
            this.Only = Only;
        }

        /// <summary>
        /// Fehlermeldung anzeigen
        /// </summary>
        /// <returns>Gibt die Fehlermeldung aus</returns>
        public string ErrorMessage()
        {
            return errorMsg;
        }

        #endregion Funktionen
    }
}
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TL.AutoIE;
using WMPLib;

namespace WindowsFormsApplication2
{

    enum OCR_SYSTEM_CURSORS : uint
    {
        /// <summary>
        /// Standard arrow and small hourglass
        /// </summary>
        OCR_APPSTARTING = 32650,

        /// <summary>
        /// Standard arrow
        /// </summary>
        OCR_NORMAL = 32512,

        /// <summary>
        /// Crosshair
        /// </summary>
        OCR_CROSS = 32515,

        /// <summary>
        /// Windows 2000/XP: Hand
        /// </summary>
        OCR_HAND = 32649,

        /// <summary>
        /// Arrow and question mark
        /// </summary>
        OCR_HELP = 32651,

        /// <summary>
        /// I-beam
        /// </summary>
        OCR_IBEAM = 32513,

        /// <summary>
        /// Slashed circle
        /// </summary>
        OCR_NO = 32648,

        /// <summary>
        /// Four-pointed arrow pointing north, south, east, and west
        /// </summary>
        OCR_SIZEALL = 32646,

        /// <summary>
        /// Double-pointed arrow pointing northeast and southwest
        /// </summary>
        OCR_SIZENESW = 32643,

        /// <summary>
        /// Double-pointed arrow pointing north and south
        /// </summary>
        OCR_SIZENS = 32645,

        /// <summary>
        /// Double-pointed arrow pointing northwest and southeast
        /// </summary>
        OCR_SIZENWSE = 32642,

        /// <summary>
        /// Double-pointed arrow pointing west and east
        /// </summary>
        OCR_SIZEWE = 32644,

        /// <summary>
        /// Vertical arrow
        /// </summary>
        OCR_UP = 32516,

        /// <summary>
        /// Hourglass
        /// </summary>
        OCR_WAIT = 32514
    }

    enum IDC_STANDARD_CURSORS : int
    {
        IDC_ARROW = 32512,
        IDC_IBEAM = 32513,
        IDC_WAIT = 32514,
        IDC_CROSS = 32515,
        IDC_UPARROW = 32516,
        IDC_SIZE = 32640,
        IDC_ICON = 32641,
        IDC_SIZENWSE = 32642,
        IDC_SIZENESW = 32643,
        IDC_SIZEWE = 32644,
        IDC_SIZENS = 32645,
        IDC_SIZEALL = 32646,
        IDC_NO = 32648,
        IDC_APPSTARTING = 32650,
        IDC_HELP = 32651
    }


}
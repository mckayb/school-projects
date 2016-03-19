﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by coded UI test builder.
//      Version: 14.0.0.0
//
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------

namespace CodedUITestProject1
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    
    
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public partial class UIMap
    {
        
        /// <summary>
        /// Opens up the tip calculator, tests a bill cost of 20 and a tip percent of 20 and then computes the tip.
        /// </summary>
        public void TipCalculatorWalkthroughTest()
        {
            #region Variable Declarations
            WinEdit uIBillTotalInputEdit = this.UITipCalculatorWindow.UIBillTotalInputWindow.UIBillTotalInputEdit;
            WinButton uIComputeTipButton = this.UITipCalculatorWindow.UIComputeTipWindow.UIComputeTipButton;
            #endregion

            // Launch '%USERPROFILE%\Source\Repos\cs3500\Tip_Calculator\Tip_Calculator\bin\Debug\Tip_Calculator.exe'
            ApplicationUnderTest uITipCalculatorWindow = ApplicationUnderTest.Launch(this.TipCalculatorWalkthroughTestParams.UITipCalculatorWindowExePath, this.TipCalculatorWalkthroughTestParams.UITipCalculatorWindowAlternateExePath);

            // Type '20' in 'billTotalInput' text box
            uIBillTotalInputEdit.Text = this.TipCalculatorWalkthroughTestParams.UIBillTotalInputEditText;

            // Click 'Compute Tip' button
            Mouse.Click(uIComputeTipButton, new Point(55, 17));
        }
        
        /// <summary>
        /// Just checks that after we add a bill total of 20 with a 20% tip, the total cost is $24.00
        /// </summary>
        public void AssertBillTotalCorrect()
        {
            #region Variable Declarations
            WinEdit uITotalCostInputEdit = this.UITipCalculatorWindow.UITotalCostInputWindow.UITotalCostInputEdit;
            #endregion

            // Verify that the 'Text' property of 'totalCostInput' text box equals '$24.00'
            Assert.AreEqual(this.AssertBillTotalCorrectExpectedValues.UITotalCostInputEditText, uITotalCostInputEdit.Text, "Calculated the wrong total bill.");
        }
        
        /// <summary>
        /// Tests that changing the tip percent works too.
        /// </summary>
        public void AssertChangingTipPercentCalculation()
        {
            #region Variable Declarations
            WinEdit uIBillTotalInputEdit = this.UITipCalculatorWindow.UIBillTotalInputWindow.UIBillTotalInputEdit;
            WinEdit uITipPercentInputEdit = this.UITipCalculatorWindow.UIItem20Window.UITipPercentInputEdit;
            WinButton uIComputeTipButton = this.UITipCalculatorWindow.UIComputeTipWindow.UIComputeTipButton;
            #endregion

            // Launch '%USERPROFILE%\Source\Repos\cs3500\Tip_Calculator\Tip_Calculator\bin\Debug\Tip_Calculator.exe'
            ApplicationUnderTest uITipCalculatorWindow = ApplicationUnderTest.Launch(this.AssertChangingTipPercentCalculationParams.UITipCalculatorWindowExePath, this.AssertChangingTipPercentCalculationParams.UITipCalculatorWindowAlternateExePath);

            // Type '10' in 'billTotalInput' text box
            uIBillTotalInputEdit.Text = this.AssertChangingTipPercentCalculationParams.UIBillTotalInputEditText;

            // Type '10' in 'tipPercentInput' text box
            uITipPercentInputEdit.Text = this.AssertChangingTipPercentCalculationParams.UITipPercentInputEditText;

            // Click 'Compute Tip' button
            Mouse.Click(uIComputeTipButton, new Point(41, 9));
        }
        
        /// <summary>
        /// Changes the tip percent and makes sure that works properly.
        /// </summary>
        public void TestChangingTipPercent()
        {
            #region Variable Declarations
            WinEdit uIBillTotalInputEdit = this.UITipCalculatorWindow.UIBillTotalInputWindow.UIBillTotalInputEdit;
            WinEdit uITipPercentInputEdit = this.UITipCalculatorWindow.UIItem20Window.UITipPercentInputEdit;
            WinButton uIComputeTipButton = this.UITipCalculatorWindow.UIComputeTipWindow.UIComputeTipButton;
            #endregion

            // Launch '%USERPROFILE%\Source\Repos\cs3500\Tip_Calculator\Tip_Calculator\bin\Debug\Tip_Calculator.exe'
            ApplicationUnderTest uITipCalculatorWindow = ApplicationUnderTest.Launch(this.TestChangingTipPercentParams.UITipCalculatorWindowExePath, this.TestChangingTipPercentParams.UITipCalculatorWindowAlternateExePath);

            // Type '10' in 'billTotalInput' text box
            uIBillTotalInputEdit.Text = this.TestChangingTipPercentParams.UIBillTotalInputEditText;

            // Type '10' in 'tipPercentInput' text box
            uITipPercentInputEdit.Text = this.TestChangingTipPercentParams.UITipPercentInputEditText;

            // Click 'Compute Tip' button
            Mouse.Click(uIComputeTipButton, new Point(48, 9));
        }
        
        /// <summary>
        /// Test to make sure that after changing the tip percentage, the total bill is calculated correctly.
        /// </summary>
        public void AssertTipChangePercent()
        {
            #region Variable Declarations
            WinEdit uITotalCostInputEdit = this.UITipCalculatorWindow.UITotalCostInputWindow.UITotalCostInputEdit;
            #endregion

            // Verify that the 'Text' property of 'totalCostInput' text box equals '$11.00'
            Assert.AreEqual(this.AssertTipChangePercentExpectedValues.UITotalCostInputEditText, uITotalCostInputEdit.Text, "Total cost not calculated correctly when changing tip percent.");
        }
        
        #region Properties
        public virtual TipCalculatorWalkthroughTestParams TipCalculatorWalkthroughTestParams
        {
            get
            {
                if ((this.mTipCalculatorWalkthroughTestParams == null))
                {
                    this.mTipCalculatorWalkthroughTestParams = new TipCalculatorWalkthroughTestParams();
                }
                return this.mTipCalculatorWalkthroughTestParams;
            }
        }
        
        public virtual AssertBillTotalCorrectExpectedValues AssertBillTotalCorrectExpectedValues
        {
            get
            {
                if ((this.mAssertBillTotalCorrectExpectedValues == null))
                {
                    this.mAssertBillTotalCorrectExpectedValues = new AssertBillTotalCorrectExpectedValues();
                }
                return this.mAssertBillTotalCorrectExpectedValues;
            }
        }
        
        public virtual AssertChangingTipPercentCalculationParams AssertChangingTipPercentCalculationParams
        {
            get
            {
                if ((this.mAssertChangingTipPercentCalculationParams == null))
                {
                    this.mAssertChangingTipPercentCalculationParams = new AssertChangingTipPercentCalculationParams();
                }
                return this.mAssertChangingTipPercentCalculationParams;
            }
        }
        
        public virtual TestChangingTipPercentParams TestChangingTipPercentParams
        {
            get
            {
                if ((this.mTestChangingTipPercentParams == null))
                {
                    this.mTestChangingTipPercentParams = new TestChangingTipPercentParams();
                }
                return this.mTestChangingTipPercentParams;
            }
        }
        
        public virtual AssertTipChangePercentExpectedValues AssertTipChangePercentExpectedValues
        {
            get
            {
                if ((this.mAssertTipChangePercentExpectedValues == null))
                {
                    this.mAssertTipChangePercentExpectedValues = new AssertTipChangePercentExpectedValues();
                }
                return this.mAssertTipChangePercentExpectedValues;
            }
        }
        
        public UITipCalculatorWindow UITipCalculatorWindow
        {
            get
            {
                if ((this.mUITipCalculatorWindow == null))
                {
                    this.mUITipCalculatorWindow = new UITipCalculatorWindow();
                }
                return this.mUITipCalculatorWindow;
            }
        }
        #endregion
        
        #region Fields
        private TipCalculatorWalkthroughTestParams mTipCalculatorWalkthroughTestParams;
        
        private AssertBillTotalCorrectExpectedValues mAssertBillTotalCorrectExpectedValues;
        
        private AssertChangingTipPercentCalculationParams mAssertChangingTipPercentCalculationParams;
        
        private TestChangingTipPercentParams mTestChangingTipPercentParams;
        
        private AssertTipChangePercentExpectedValues mAssertTipChangePercentExpectedValues;
        
        private UITipCalculatorWindow mUITipCalculatorWindow;
        #endregion
    }
    
    /// <summary>
    /// Parameters to be passed into 'TipCalculatorWalkthroughTest'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class TipCalculatorWalkthroughTestParams
    {
        
        #region Fields
        /// <summary>
        /// Launch '%USERPROFILE%\Source\Repos\cs3500\Tip_Calculator\Tip_Calculator\bin\Debug\Tip_Calculator.exe'
        /// </summary>
        public string UITipCalculatorWindowExePath = "C:\\Users\\mbroderi\\Source\\Repos\\cs3500\\Tip_Calculator\\Tip_Calculator\\bin\\Debug\\Tip" +
            "_Calculator.exe";
        
        /// <summary>
        /// Launch '%USERPROFILE%\Source\Repos\cs3500\Tip_Calculator\Tip_Calculator\bin\Debug\Tip_Calculator.exe'
        /// </summary>
        public string UITipCalculatorWindowAlternateExePath = "%USERPROFILE%\\Source\\Repos\\cs3500\\Tip_Calculator\\Tip_Calculator\\bin\\Debug\\Tip_Cal" +
            "culator.exe";
        
        /// <summary>
        /// Type '20' in 'billTotalInput' text box
        /// </summary>
        public string UIBillTotalInputEditText = "20";
        #endregion
    }
    
    /// <summary>
    /// Parameters to be passed into 'AssertBillTotalCorrect'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class AssertBillTotalCorrectExpectedValues
    {
        
        #region Fields
        /// <summary>
        /// Verify that the 'Text' property of 'totalCostInput' text box equals '$24.00'
        /// </summary>
        public string UITotalCostInputEditText = "$24.00";
        #endregion
    }
    
    /// <summary>
    /// Parameters to be passed into 'AssertChangingTipPercentCalculation'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class AssertChangingTipPercentCalculationParams
    {
        
        #region Fields
        /// <summary>
        /// Launch '%USERPROFILE%\Source\Repos\cs3500\Tip_Calculator\Tip_Calculator\bin\Debug\Tip_Calculator.exe'
        /// </summary>
        public string UITipCalculatorWindowExePath = "C:\\Users\\mbroderi\\Source\\Repos\\cs3500\\Tip_Calculator\\Tip_Calculator\\bin\\Debug\\Tip" +
            "_Calculator.exe";
        
        /// <summary>
        /// Launch '%USERPROFILE%\Source\Repos\cs3500\Tip_Calculator\Tip_Calculator\bin\Debug\Tip_Calculator.exe'
        /// </summary>
        public string UITipCalculatorWindowAlternateExePath = "%USERPROFILE%\\Source\\Repos\\cs3500\\Tip_Calculator\\Tip_Calculator\\bin\\Debug\\Tip_Cal" +
            "culator.exe";
        
        /// <summary>
        /// Type '10' in 'billTotalInput' text box
        /// </summary>
        public string UIBillTotalInputEditText = "10";
        
        /// <summary>
        /// Type '10' in 'tipPercentInput' text box
        /// </summary>
        public string UITipPercentInputEditText = "10";
        #endregion
    }
    
    /// <summary>
    /// Parameters to be passed into 'TestChangingTipPercent'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class TestChangingTipPercentParams
    {
        
        #region Fields
        /// <summary>
        /// Launch '%USERPROFILE%\Source\Repos\cs3500\Tip_Calculator\Tip_Calculator\bin\Debug\Tip_Calculator.exe'
        /// </summary>
        public string UITipCalculatorWindowExePath = "C:\\Users\\mbroderi\\Source\\Repos\\cs3500\\Tip_Calculator\\Tip_Calculator\\bin\\Debug\\Tip" +
            "_Calculator.exe";
        
        /// <summary>
        /// Launch '%USERPROFILE%\Source\Repos\cs3500\Tip_Calculator\Tip_Calculator\bin\Debug\Tip_Calculator.exe'
        /// </summary>
        public string UITipCalculatorWindowAlternateExePath = "%USERPROFILE%\\Source\\Repos\\cs3500\\Tip_Calculator\\Tip_Calculator\\bin\\Debug\\Tip_Cal" +
            "culator.exe";
        
        /// <summary>
        /// Type '10' in 'billTotalInput' text box
        /// </summary>
        public string UIBillTotalInputEditText = "10";
        
        /// <summary>
        /// Type '10' in 'tipPercentInput' text box
        /// </summary>
        public string UITipPercentInputEditText = "10";
        #endregion
    }
    
    /// <summary>
    /// Parameters to be passed into 'AssertTipChangePercent'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class AssertTipChangePercentExpectedValues
    {
        
        #region Fields
        /// <summary>
        /// Verify that the 'Text' property of 'totalCostInput' text box equals '$11.00'
        /// </summary>
        public string UITotalCostInputEditText = "$11.00";
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class UITipCalculatorWindow : WinWindow
    {
        
        public UITipCalculatorWindow()
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.Name] = "Tip Calculator";
            this.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.ClassName, "WindowsForms10.Window", PropertyExpressionOperator.Contains));
            this.WindowTitles.Add("Tip Calculator");
            #endregion
        }
        
        #region Properties
        public UIBillTotalInputWindow UIBillTotalInputWindow
        {
            get
            {
                if ((this.mUIBillTotalInputWindow == null))
                {
                    this.mUIBillTotalInputWindow = new UIBillTotalInputWindow(this);
                }
                return this.mUIBillTotalInputWindow;
            }
        }
        
        public UIComputeTipWindow UIComputeTipWindow
        {
            get
            {
                if ((this.mUIComputeTipWindow == null))
                {
                    this.mUIComputeTipWindow = new UIComputeTipWindow(this);
                }
                return this.mUIComputeTipWindow;
            }
        }
        
        public UITotalCostInputWindow UITotalCostInputWindow
        {
            get
            {
                if ((this.mUITotalCostInputWindow == null))
                {
                    this.mUITotalCostInputWindow = new UITotalCostInputWindow(this);
                }
                return this.mUITotalCostInputWindow;
            }
        }
        
        public UIItem20Window UIItem20Window
        {
            get
            {
                if ((this.mUIItem20Window == null))
                {
                    this.mUIItem20Window = new UIItem20Window(this);
                }
                return this.mUIItem20Window;
            }
        }
        #endregion
        
        #region Fields
        private UIBillTotalInputWindow mUIBillTotalInputWindow;
        
        private UIComputeTipWindow mUIComputeTipWindow;
        
        private UITotalCostInputWindow mUITotalCostInputWindow;
        
        private UIItem20Window mUIItem20Window;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class UIBillTotalInputWindow : WinWindow
    {
        
        public UIBillTotalInputWindow(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.ControlName] = "billTotalInput";
            this.WindowTitles.Add("Tip Calculator");
            #endregion
        }
        
        #region Properties
        public WinEdit UIBillTotalInputEdit
        {
            get
            {
                if ((this.mUIBillTotalInputEdit == null))
                {
                    this.mUIBillTotalInputEdit = new WinEdit(this);
                    #region Search Criteria
                    this.mUIBillTotalInputEdit.WindowTitles.Add("Tip Calculator");
                    #endregion
                }
                return this.mUIBillTotalInputEdit;
            }
        }
        #endregion
        
        #region Fields
        private WinEdit mUIBillTotalInputEdit;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class UIComputeTipWindow : WinWindow
    {
        
        public UIComputeTipWindow(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.ControlName] = "computeTipButton";
            this.WindowTitles.Add("Tip Calculator");
            #endregion
        }
        
        #region Properties
        public WinButton UIComputeTipButton
        {
            get
            {
                if ((this.mUIComputeTipButton == null))
                {
                    this.mUIComputeTipButton = new WinButton(this);
                    #region Search Criteria
                    this.mUIComputeTipButton.SearchProperties[WinButton.PropertyNames.Name] = "Compute Tip";
                    this.mUIComputeTipButton.WindowTitles.Add("Tip Calculator");
                    #endregion
                }
                return this.mUIComputeTipButton;
            }
        }
        #endregion
        
        #region Fields
        private WinButton mUIComputeTipButton;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class UITotalCostInputWindow : WinWindow
    {
        
        public UITotalCostInputWindow(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.ControlName] = "totalCostInput";
            this.WindowTitles.Add("Tip Calculator");
            #endregion
        }
        
        #region Properties
        public WinEdit UITotalCostInputEdit
        {
            get
            {
                if ((this.mUITotalCostInputEdit == null))
                {
                    this.mUITotalCostInputEdit = new WinEdit(this);
                    #region Search Criteria
                    this.mUITotalCostInputEdit.SearchProperties[WinEdit.PropertyNames.Name] = "Total Cost";
                    this.mUITotalCostInputEdit.WindowTitles.Add("Tip Calculator");
                    #endregion
                }
                return this.mUITotalCostInputEdit;
            }
        }
        #endregion
        
        #region Fields
        private WinEdit mUITotalCostInputEdit;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class UIItem20Window : WinWindow
    {
        
        public UIItem20Window(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.ControlName] = "tipPercentInput";
            this.WindowTitles.Add("Tip Calculator");
            #endregion
        }
        
        #region Properties
        public WinEdit UITipPercentInputEdit
        {
            get
            {
                if ((this.mUITipPercentInputEdit == null))
                {
                    this.mUITipPercentInputEdit = new WinEdit(this);
                    #region Search Criteria
                    this.mUITipPercentInputEdit.SearchProperties[WinEdit.PropertyNames.Name] = "Tip Percent";
                    this.mUITipPercentInputEdit.WindowTitles.Add("Tip Calculator");
                    #endregion
                }
                return this.mUITipPercentInputEdit;
            }
        }
        #endregion
        
        #region Fields
        private WinEdit mUITipPercentInputEdit;
        #endregion
    }
}

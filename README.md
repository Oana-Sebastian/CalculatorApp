# 🧮 **C#-CalculatorApp**

CalculatorApp is a WPF-based calculator application built in C# using the MVVM design pattern. It emulates the standard Windows Calculator and includes additional features from the Programmer variant, making it a versatile tool for everyday calculations and programming-related numeral conversions.

# 📌 **Features**

➗ **Standard Operations**
  * Supports basic arithmetic operations such as **addition**, **subtraction**, **multiplication**, and **division**.

🔢 **Additional Operations**:

  * Includes operations like **square root**, **reciprocal** (1/x), **square**, **percentage**, and **sign inversion** (+/-).
  * 
💾 **Memory Functions**:

  * Offers memory controls: **MC**, **MR**, **M+**, **M-**, **MS**, **Mv** with the same behavior as the Windows Calculator.

🔢 **Digit Grouping**:

  * Automatically groups digits (e.g., 30000 is displayed as 30,000) based on the current culture.
  * Digit grouping is applied in **real-time** as you enter numbers, and all **decimal places** are preserved.

⚠️ **Error Handling**:
     
  * Robust error management that displays **"Error"** (e.g., on division by zero) and **disables** further operations until the error is cleared.

🖥️ **Programmer Mode**:

Provides conversion functionalities between numeral systems:
  
  * **Binary**
  * **Octal**
  * **Decimal**
  * **Hexadecimal**
       
with **input validation**.

✂️ **Custom Cut/Copy/Paste**:
  
  * Implements custom text manipulation for **cut**, **copy**, and **paste** operations without relying on the pre-built textbox functionalities.

⌨️ 🖱️ **Keyboard & Mouse Input**:

  * Fully supports input via both **keyboard** and **mouse**.

🔒 **State Persistence**:
  
  * Remembers the **last used settings** (digit grouping and mode) between sessions.

# 🛠️ **Technologies**

  * **WPF & XAML**: For building a UI that mimics the Windows Calculator.
  * C# & .NET: Core programming language and platform.
  * MVVM Pattern: To separate **UI** from **business logic** and enhance maintainability.
  
# 🚀 **Getting Started**

🔑 **Prerequisites**

  * [Visual Studio](https://visualstudio.microsoft.com/) with the **.NET desktop development** workload.
  * **.NET Framework** or **.NET Core** (depending on your project settings).
       
# 📥 **Installation**
  1. Clone the Repository: https://github.com/Oana-Sebastian/CalculatorApp.git 
  2. Open the solution file (`CalculatorApp.sln`) in **Visual Studio**.
  3. **Build** the project and run it to see the Calculator in action.

# 📊 **Usage**
  * **Basic Calculations**:
      - Use the number pad and operation buttons to perform calculations.
  * **Error Handling**:
      - If an **error** occurs (for example, division by zero), the display will show **"Error"** and operator buttons will be disabled until you clear the error using the **C/CE** buttons.
  * **Digit Grouping**:
      - As you enter numbers, **digit grouping** is automatically applied based on your system's culture settings, making large numbers easier to read.
  * **Programmer Mode**:
      - Switch to **programmer mode** to convert numbers between **binary**, **octal**, **decimal**, and **hexadecimal** bases.

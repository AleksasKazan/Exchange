# Exchange
**Exchange is a console application for currency exchange calculations.**

## Getting Started

To use the Exchange tool, follow these steps:

### 1. Install the NuGet Package:

You can find the NuGet package for Exchange [here](https://www.nuget.org/packages/CurrencyExchangeHomeworkApp/1.0.0)

Install it globally using the following command:
`dotnet tool install --global CurrencyExchangeHomeworkApp --version 1.0.0`

### 2. Prepare Configuration Files:
- Download the `appsettings.json` file from [here](https://github.com/AleksasKazan/Exchange/blob/master/Exchange/appsettings.json).

- Download the `rates.json` file from [here](https://github.com/AleksasKazan/Exchange/blob/master/Exchange/rates.json).
 
  ![image](https://github.com/AleksasKazan/Exchange/assets/82649971/043fbe02-71a9-4cfe-885a-1521684f6945)
- Optionally, if you want to customize exchange rates, modify download `rates.json` file as needed.

### 3. Place Configuration Files:

Drop the downloaded `appsettings.json` and the `rates.json` files into the directory where you want to run the Exchange app.   

### 4. Run the Application:

Start the Exchange app by executing the following command: `Exchange`

![image](https://github.com/AleksasKazan/Exchange/assets/82649971/243aa261-c476-429d-a9eb-331f20e91ecd)

The tool will use the configuration files you provided to perform currency exchange calculations.

## Example

Here's a screenshot of the Exchange tool in action:
![image](https://github.com/AleksasKazan/Exchange/assets/82649971/f4c9922d-b3dd-4e70-9243-b08123a416b8)

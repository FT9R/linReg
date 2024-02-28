# linReg

This is a console [application](https://github.com/FT9R/linReg/tree/main/bin/Release/net8.0) designed to find the best-fit linear equation $y_i = ax_i + b$  
for a [samples](https://github.com/FT9R/linReg/blob/main/bin/Release/net8.0/samples.csv) dataset using the least-squares approach.  
$x_i$ and $y_i$ are independent and dependent values from the dataset, respectively.  
$a$ - the slope of the line.  
$b$ - the y-intercept.  

Expressions to derive $a$ and $b$ used by Statistics.dll: 
$$a = \bar y - (b \bar x)$$
$$b = \frac{\sum\limits_{i=1}^n (x_i - \bar x)(y_i - \bar y)} {\sum\limits_{i=1}^n (x_i - \bar x)^2}$$

## Dependencies
[.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

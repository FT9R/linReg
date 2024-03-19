# linReg

A console [application](https://github.com/FT9R/linReg/tree/main/bin/Release/net8.0) that's able to find the best-fit linear equation $y_i = ax_i + b$  
for a two-dimensional [samples](https://github.com/FT9R/linReg/blob/main/bin/Release/net8.0/samples.csv) dataset using the least-squares approach.  
$x_i$ and $y_i$ are independent and dependent values from the dataset, respectively.  
$a$ - the slope of the line.  
$b$ - the y-intercept.  

## Expressions used by Statistics.dll
Slope coefficient equation:  
$$a = \frac{\sum\limits_{i=1}^n (x_i - \bar x)(y_i - \bar y)} {\sum\limits_{i=1}^n (x_i - \bar x)^2}$$

y-intercept coefficient equation:  
$$b = \bar y - (a \bar x)$$

Definition of the coefficient of determination:
$$R^2 = 1 - \frac{SS_{res}} {SS_{tot}}$$

Residual sum of squares:
$$SS_{res} = \sum_{i=1}^n (y_i - f_i)^2$$
Where $f_i$ is fitted value equal to $x_i * a + b$.

Total sum of squares:
$$SS_{tot} = \sum_{i=1}^n (y_i - \bar y)^2$$


## Dependencies
[.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
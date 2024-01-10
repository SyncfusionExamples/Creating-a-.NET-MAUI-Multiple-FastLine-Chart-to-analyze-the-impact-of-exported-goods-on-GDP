# Creating a .NET MAUI Multiple FastLine Chart to analyze the impact of exported goods on GDP
This sample illustrates the creation of a .NET MAUI Multiple FastLine Chart for analyzing the influence of exported goods on GDP from 1827 to 2014.

![Presentation1](https://github.com/SyncfusionExamples/Creating-a-.NET-MAUI-Multiple-FastLine-Chart-to-analyze-the-impact-of-exported-goods-on-GDP/assets/105496706/abbf3e3c-b9fe-4814-a266-9d6af3abbda2)


## Step 1: Gathering yearly data for exported goods 
First, let’s gather the exported goods data annually across countries and world regions from [Our World in Data](https://ourworldindata.org/weve-updated-our-entry-on-trade-and-globalization) for the period 1827 to 2014.

## Step 2: Preparing the data for the chart
Create a Model class that includes properties storing the year, GDP percentage value, and Country.

**C#**
```
public class Model
{
    public double Year { get; set; }
    public double Value { get; set; }
    public string Country { get; set; }

    public Model(double year, double value, string country)
    {
        Year = year;
        Value = value;
        Country = country;
    }
}
```

Next, annually generate the exported goods data as a collection of the share of GDP ratios across countries and world regions using the ViewModel class. 
The CSV data assigned to the AllCountriesData property holds information for all countries, including year and its corresponding GDP percentage value, utilizing the ReadCSV method. Meanwhile, the BrazilData, ChinaData, UKData, USData, and WorldData properties individually store export goods data for Brazil, China, the UK, the US, and the World, respectively.

Refer to the following code example.

**C#**
```
public class ViewModel
{
    public List<Model>? AllCountriesData { get; set; }
    public List<Model>? BrazilData { get; set; }
    public List<Model>? ChinaData { get; set; }
    public List<Model>? UKData { get; set; }
    public List<Model>? USData { get; set; }
    public List<Model>? WorldData { get; set; }
    public List<Model>? LastIndexData { get; set; }

    public float Spacing => 5;

    public ViewModel()
    {
        AllCountriesData = new List<Model>(ReadCSV());
        BrazilData = AllCountriesData.Where(d => d.Country == "Brazil").ToList();
        ChinaData = AllCountriesData.Where(d => d.Country == "China").ToList();
        UKData = AllCountriesData.Where(d => d.Country == "United Kingdom").ToList();
        USData = AllCountriesData.Where(d => d.Country == "United States").ToList();
        WorldData = AllCountriesData.Where(d => d.Country == "World").ToList();
    }

    public static IEnumerable<Model> ReadCSV()
    {
        Assembly executingAssembly = typeof(App).GetTypeInfo().Assembly;
        Stream inputStream = executingAssembly.GetManifestResourceStream("IndustrialRobotAnalysis.Resources.Raw.exportdata.csv");

        string line;
        List<string> lines = new();

        using StreamReader reader = new(inputStream);
        while ((line = reader.ReadLine()) != null)
        {
            lines.Add(line);
        }

        return lines.Select(line =>
        {
            string[] data = line.Split(',');

            return new Model(Convert.ToDouble(data[0]), Math.Round(Convert.ToDouble(data[1]), 1), data[2]);
        });
    }
}
```

## Step 3: Configuring the Syncfusion .NET MAUI Cartesian Charts
Configure the Syncfusion .NET MAUI Cartesian Charts control using this [user guide documentation](https://help.syncfusion.com/maui/cartesian-charts/getting-started).

Refer to the following code example.

**XAML**
```
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage 
….
 xmlns:chart="clr-namespace:Syncfusion.Maui.Charts;assembly=Syncfusion.Maui.Charts"
 xmlns:local="clr-namespace:ExportAnalysis">

<chart:SfCartesianChart>
    
    <!--Initialize the x axis for chart-->
    <chart:SfCartesianChart.XAxes>
        <chart:NumericalAxis/>
    </chart:SfCartesianChart.XAxes>

    <!--Initialize the y axis for chart-->
    <chart:SfCartesianChart.YAxes>
        <chart:NumericalAxis/>

         <!—Additional custom y axis-->
        <local:NumericalAxisExt/>
    </chart:SfCartesianChart.YAxes>

</chart:SfCartesianChart>
```

## Step 4: Initialize the BindingContext for the chart
Configure the ViewModel class of your XAML page to bind its properties to the BindingContext of the SfCartesianChart.

Refer to the following code example.

**XAML**
```
<chart:SfCartesianChart.BindingContext>
   <local:ViewModel/>
</chart:SfCartesianChart.BindingContext>
```

## Step 5: Bind the annual exported goods data as a share of the GDP ratio to the FastLine graph
To visualize the value of merchandise exported goods as a share of the GDP ratio across countries and world regions, we’ll use the Syncfusion [FastLine Chart](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.FastLineSeries.html) instance.

Refer to the following code example.

**XAML**
```
<chart:FastLineSeries ItemsSource="{Binding BrazilData}" 
                      XBindingPath="Year"
                      YBindingPath="Value"/>

<chart:FastLineSeries ItemsSource="{Binding ChinaData}" 
                      XBindingPath="Year"
                      YBindingPath="Value"/>

<chart:FastLineSeries ItemsSource="{Binding UKData}" 
                      XBindingPath="Year"
                      YBindingPath="Value"/>

<chart:FastLineSeries ItemsSource="{Binding USData}" 
                      XBindingPath="Year"
                      YBindingPath="Value"/>

<chart:FastLineSeries ItemsSource="{Binding WorldData}" 
                      XBindingPath="Year"
                      YBindingPath="Value"/>
```

In the previous code example, we utilized multiple [FastLine series](https://help.syncfusion.com/maui/cartesian-charts/fastline) to analyze the share of GDP ratio across countries and world regions. This involved maintaining separate series for each country and the entire world. We bound the [ItemSource](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.ChartSeries.html#Syncfusion_Maui_Charts_ChartSeries_ItemsSource) to the ViewModel class properties, namely BrazilData, ChinaData, UKData, USData, and WorldData. Additionally, we specified the [XBindingPath](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.ChartSeries.html#Syncfusion_Maui_Charts_ChartSeries_XBindingPath) with the Year property and the [YBindingPath](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.ChartSeries.html#Syncfusion_Maui_Charts_ChartSeries_XBindingPath) with the GDP ratio Value property.

## Step 6: Multi-Axis integration
As mentioned earlier, for this demo, we'll utilize multiple fast-line series to analyze the share of GDP ratio across countries and world regions. To achieve this, we will use a shared x-axis and two y-axes — one for analyzing GDP growth percentage values and another for chart customization.

Cartesian charts provides support to arrange the multiple series inside the same chart area with specified x-axis and y-axis. [Multiple axes](https://help.syncfusion.com/maui/cartesian-charts/axis/types#multiple-axes) can be arranged in a stacking order or in a side by side pattern. In this demo, the additional y axis is added at the last index value of X Axes using [CrossesAt](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.ChartAxis.html#Syncfusion_Maui_Charts_ChartAxis_CrossesAt) property of the [ChartAxis](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.ChartAxis.html).

Refer to the following code example to add an additional y-axis for chart customization and place it at the last index of X Axes. 

**XAML**
```
<chart:SfCartesianChart.YAxes>
                
     <chart:NumericalAxis>
     </chart:NumericalAxis>

     <local:NumericalAxisExt CrossesAt="{Static x:Double.MaxValue}">               
     </local:NumericalAxisExt>

</chart:SfCartesianChart.YAxes>
```

## Step 7: Customizing the chart appearance
We can enhance the appearance of the MAUI multiple fast line series by customizing its elements.  

### Customize the chart background:
We have placed the chart inside a border.

**XAML**
```
<Border StrokeShape="RoundRectangle 20" 
             StrokeThickness="2"
             Stroke="Gray"
             Margin="{OnPlatform WinUI='20, 0, 20, 20', Android='10', iOS='10', MacCatalyst='30'}">
        
    <chart:SfCartesianChart>
….
    </chart:SfCartesianChart>

</Border>
```

### Personalized titles
Refer to the following code example to customize the Chart title using the [Title](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.ChartAxis.html) property.

**XAML**
```
<chart:SfCartesianChart.Title>
    
    <Grid Margin="0, 10, 0, 10">
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="2*"/>
            <ColumnDefinition Width="{OnPlatform Android=49*,WinUI=59*,MacCatalyst=59*,iOS=49*}"/>
        </Grid.ColumnDefinitions>
        <Image Source="export.png" WidthRequest="{OnPlatform Default='40', MacCatalyst='50'}" 
               Margin="25, 0, 0, 0" HeightRequest="{OnPlatform Default='40', MacCatalyst='50'}" 
               Grid.Column="0" Grid.RowSpan="2"/>
        <VerticalStackLayout Grid.Column="1" Margin="20, 0, 0, 0">
            <Label Text=" Exported Goods Contribution to GDP from 1827 to 2014" 
                   FontSize="{OnPlatform Default='20', Android='18', iOS='18'}" 
                   Padding="{OnPlatform Default='0,0,5,5', Android='0,0,5,0', iOS='0,0,5,0'}" 
                   HorizontalTextAlignment="Start"/>
            <Label Text="Total value of merchandise exports divided by gross domestic product, expressed as a percentage." 
                   HorizontalTextAlignment="Start" FontSize="{OnPlatform Android=13,WinUI=15,MacCatalyst=16,iOS=13}" 
                   TextColor="Grey" />
        </VerticalStackLayout>
    </Grid>
    
</chart:SfCartesianChart.Title>
```

### Customize the chart axes
Refer to the following code example to customize the chart axis using [LabelStyle](https://help.syncfusion.com/maui/cartesian-charts/axis/axislabels#label-customization), [AxisLineStyle](https://help.syncfusion.com/maui/cartesian-charts/axis/axisline#customization), [MajorTickStyle](https://help.syncfusion.com/maui/cartesian-charts/axis/ticklines) and [MajorGridLineStyle](https://help.syncfusion.com/maui/cartesian-charts/axis/gridlines#customization) properties.

**XAML**
```
<chart:SfCartesianChart>

    <chart:SfCartesianChart.Resources>
        <DoubleCollection x:Key="gridLine">
            <x:Double>3</x:Double>
            <x:Double>3</x:Double>
        </DoubleCollection>

    </chart:SfCartesianChart.Resources>

    <!--X axis customization-->
    <chart:SfCartesianChart.XAxes>
       <chart:NumericalAxis ShowMajorGridLines="False"/>
    </chart:SfCartesianChart.XAxes>

    <chart:SfCartesianChart.YAxes>
    
       <!--Y axis customization-->
    <chart:NumericalAxis Interval="10" Maximum="58">
        <chart:NumericalAxis.LabelStyle>
            <chart:ChartAxisLabelStyle LabelFormat="0'%"/>
        </chart:NumericalAxis.LabelStyle>
        <chart:NumericalAxis.AxisLineStyle>
            <chart:ChartLineStyle StrokeWidth="0"/>
        </chart:NumericalAxis.AxisLineStyle>
        <chart:NumericalAxis.MajorTickStyle>
            <chart:ChartAxisTickStyle StrokeWidth="0"/>
        </chart:NumericalAxis.MajorTickStyle>
        <chart:NumericalAxis.MajorGridLineStyle>
            <chart:ChartLineStyle StrokeDashArray="{StaticResource gridLine}"/>
        </chart:NumericalAxis.MajorGridLineStyle>
    </chart:NumericalAxis>

    <!—Additional cutom Y axis-->
    <local:NumericalAxisExt CrossesAt="{Static x:Double.MaxValue}" 
                 LabelExtent="{OnPlatform Default='100', MacCatalyst='110'}">
    </local:NumericalAxisExt>
</chart:SfCartesianChart.YAxes>
```

### Custom axis rendering
We can customize the default chart axis rendering to meet specific requirements by extending the necessary axis and overriding the DrawAxis methods. Within these methods, we can implement our specific requirements. In this demo, we have added series labels at the required positions.

**C#**
```
public class NumericalAxisExt : NumericalAxis
{
    protected override void DrawAxis(ICanvas canvas, Rect arrangeRect)
    {
        var axis = this as ChartAxis;

        if (axis.Parent is SfCartesianChart chart && BindingContext is ViewModel viewModel)
        {
            var x = arrangeRect.X + viewModel.Spacing;

            foreach (CartesianSeries series in chart.Series)
            {
                var style = new ChartAxisLabelStyle() { FontSize = 12,TextColor = (series.Fill as SolidColorBrush).Color,  Parent = this.Parent};
                OnDrawLabels(canvas, series, style, (float)x);
            }
        }
    }
}
```

### Customize the chart series
We can customize the series elements, including color and size, using the [Fill](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.ChartSeries.html#Syncfusion_Maui_Charts_ChartSeries_Fill) and [StrokeWidth](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.XYDataSeries.html#Syncfusion_Maui_Charts_XYDataSeries_StrokeWidth) properties.

**XAML**
```
<chart:FastLineSeries StrokeWidth="2" Fill="Red" Label="World"/>
                      
<chart:FastLineSeries StrokeWidth="2" Fill="Blue" Label="China"/>
          
<chart:FastLineSeries StrokeWidth="2" Fill="ForestGreen" Label="United Kingdom"/>

<chart:FastLineSeries StrokeWidth="2" Fill="DarkViolet" Label="Brazil"/>
 
<chart:FastLineSeries StrokeWidth="2" Fill="Firebrick" Label="United States"/>
```

### Adding interactivity to the chart
To enhance our chart’s readability with additional information, let’s add and customize the trackball using the [TrackballCreated](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.SfCartesianChart.html#Syncfusion_Maui_Charts_SfCartesianChart_TrackballCreated) callback event.

**XAML**
```
<chart:SfCartesianChart TrackballCreated="SfCartesianChart_TrackballCreated">

     <chart:SfCartesianChart.XAxes>
         <chart:NumericalAxis ShowTrackballLabel="True"/>
     </chart:SfCartesianChart.XAxes>

     <chart:SfCartesianChart.TrackballBehavior>
         <chart:ChartTrackballBehavior/>
     </chart:SfCartesianChart.TrackballBehavior>

 </chart:SfCartesianChart>
```

**C#**
```
private void SfCartesianChart_TrackballCreated(object sender, TrackballEventArgs e)
{
    var trackballInfoList = e.TrackballPointsInfo;

    foreach (var trackballInfo in trackballInfoList)
    {
        string prefix = trackballInfo.Series.Label == "United Kingdom" ? "UK" :
        trackballInfo.Series.Label == "United States" ? "US" :
        trackballInfo.Series.Label;
        trackballInfo.Label = $"{prefix} : {trackballInfo.Label}%";

        ChartMarkerSettings chartMarkerSettings = new ChartMarkerSettings();
        chartMarkerSettings.Stroke = trackballInfo.Series.Fill;
        chartMarkerSettings.StrokeWidth = 2;
        chartMarkerSettings.Fill = Colors.White;

        trackballInfo.MarkerSettings = chartMarkerSettings;
    }
}
```

After executing the previous code examples, the output will look like the following image.

![Presentation1](https://github.com/SyncfusionExamples/Creating-a-.NET-MAUI-Multiple-FastLine-Chart-to-analyze-the-impact-of-exported-goods-on-GDP/assets/105496706/5c54d50f-8844-420e-a4b8-497773a6f7ec)



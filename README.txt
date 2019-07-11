# kr.bbon.Xamarin.Forms

Xamarin.Forms 프로젝트를 시작하면 지속적으로 작성하던 코드를 라이브러리로 분리해서 nuget package 로 제공합니다.

## Xamarin.Essentials

Android:

링크 페이지의 가이드를 확인하고, 안드로이드 프로젝트의 해당 파일에 필요한 코드를 추가해야 합니다.

Follow short guide at: 
[http://aka.ms/essentials-getstarted](http://aka.ms/essentials-getstarted)

```csharp
protected override void OnCreate(Bundle savedInstanceState) 
{
    //...
    base.OnCreate(savedInstanceState);
    Xamarin.Essentials.Platform.Init(this, savedInstanceState); // add this line to your code, it may also be called: bundle
    //...
}

public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
{
    Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
}
```

## Plugin.Permissions

링크 페이지의 가이드를 확인하고, 안드로이드 프로젝트의 해당 파일에 필요한 코드를 추가해야 합니다.

Check a README.md:
[https://github.com/jamesmontemagno/PermissionsPlugin](https://github.com/jamesmontemagno/PermissionsPlugin)

Android:

```csharp
public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
{
	PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
	base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
}
```

This plugin uses the [Current Activity Plugin](https://github.com/jamesmontemagno/CurrentActivityPlugin/blob/master/README.md) to get access to the current Android Activity. Be sure to complete the full setup if a MainApplication.cs file was not automatically added to your application. Please fully read through the [Current Activity Plugin Documentation](https://github.com/jamesmontemagno/CurrentActivityPlugin/blob/master/README.md). At an absolute minimum you must set the following in your Activity's OnCreate method:

```csharp
Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);
```

iOS:

Due to API usage it is required to add the Calendar permission.

```
<key>NSCalendarsUsageDescription</key>
<string>Needs Calendar Permission</string>
```
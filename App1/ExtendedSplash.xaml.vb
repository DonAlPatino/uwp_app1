Imports Windows.UI.Core
Public NotInheritable Class ExtendedSplash
    Inherits Page
    Friend splashImageRect As Rect
    Private splash As SplashScreen
    Friend ScaleFactor As Double
    Friend statusBarRect As Rect
    Public Sub New(splashscreen As SplashScreen)
        InitializeComponent()
        AddHandler Window.Current.SizeChanged, AddressOf ExtendedSplash_OnResize
        splash = splashscreen
        If Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar") Then
            statusBarRect = StatusBar.GetForCurrentView.OccludedRect
        End If
        If splash IsNot Nothing Then
            AddHandler splash.Dismissed, AddressOf DismissedEventHandler
            ScaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel
            splashImageRect = splash.ImageLocation
            SetSizeImage()
        End If
        ImageRotateAnimation.Begin()
    End Sub
    Private Sub SetSizeImage()
        SplashScreenImage1.Height = splashImageRect.Height / ScaleFactor
        SplashScreenImage1.Width = splashImageRect.Width / ScaleFactor
        SplashScreenImage1.Margin = New Thickness(0, -statusBarRect.Height, 0, 0)

        SplashScreenImage2.Width = splashImageRect.Width / ScaleFactor * 0.3073
        SplashScreenImage2.Margin = New Thickness(-splashImageRect.Width / ScaleFactor * 0.547, -splashImageRect.Width / ScaleFactor * 0.1633 - statusBarRect.Height, 0, 0)

        SplashScreenImage3.Height = splashImageRect.Height / ScaleFactor
        SplashScreenImage3.Width = splashImageRect.Width / ScaleFactor
        SplashScreenImage3.Margin = New Thickness(0, -statusBarRect.Height, 0, 0)

        ScaleFactor = 1
    End Sub

    Private Async Sub DismissedEventHandler(sender As SplashScreen, args As Object)
        Await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, New DispatchedHandler(AddressOf DismissExtendedSplash))
    End Sub

    Private Sub ExtendedSplash_OnResize(sender As Object, e As WindowSizeChangedEventArgs)
        If splash IsNot Nothing Then
            splashImageRect = splash.ImageLocation
            SetSizeImage()
        End If
    End Sub
    Private Async Sub DismissExtendedSplash()
        LoadingSatatusTextBox.Text = "Загрузка данных..."
        Await Task.Delay(New TimeSpan(0, 0, 15))

        Dim rootFrame As New Frame
        rootFrame.Navigate(GetType(MainPage))
        Window.Current.Content = rootFrame
    End Sub

End Class
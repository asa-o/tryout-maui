﻿using tryoiutmaui.pages;

namespace tryoiutmaui;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private void OnGotoChatClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new OpenAiChat());
    }
}

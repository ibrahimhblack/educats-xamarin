﻿using EduCATS.Helpers.Devices;
using EduCATS.Helpers.Dialogs;
using EduCATS.Helpers.Pages;
using EduCATS.Pages.Testing.Passing.ViewModels;
using EduCATS.Pages.Testing.Passing.Views.ViewCells;
using EduCATS.Themes;
using Nyxbull.Plugins.CrossLocalization;
using Xamarin.Forms;

namespace EduCATS.Pages.Testing.Passing.Views
{
	public class TestPassingPageView : ContentPage
	{
		const double _buttonHeight = 50;

		public TestPassingPageView(int testId, bool forSelfStudy, bool fromComplexLearning = false)
		{
			BackgroundColor = Color.FromHex(Theme.Current.AppBackgroundColor);
			BindingContext = new TestPassingPageViewModel(
				new AppDialogs(), new AppPages(), new AppDevice(),
				testId, forSelfStudy, fromComplexLearning);
			this.SetBinding(TitleProperty, "Title");
			setToolbar();
			createViews();
		}

		void setToolbar()
		{
			var toolbarItem = new ToolbarItem {
				IconImageSource = ImageSource.FromFile(Theme.Current.BaseCloseIcon)
			};

			toolbarItem.SetBinding(MenuItem.CommandProperty, "CloseCommand");
			ToolbarItems.Add(toolbarItem);
		}

		void createViews()
		{
			var listView = createQuestionList();
			var buttonLayout = createButtonLayout();

			var mainLayout = new StackLayout {
				Children = {
					listView, buttonLayout
				}
			};

			mainLayout.SetBinding(IsEnabledProperty, "IsNotLoading");
			Content = mainLayout;
		}

		Grid createButtonLayout()
		{
			var acceptButton = createButton(
				CrossLocalization.Translate("test_passing_answer"),
				"AnswerCommand");

			var skipButton = createButton(
				CrossLocalization.Translate("test_passing_skip"),
				"SkipCommand");

			var buttonGridLayout = new Grid {
				HeightRequest = 100,
				VerticalOptions = LayoutOptions.EndAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness(20),
				BackgroundColor = Color.FromHex(Theme.Current.CommonBlockColor),
				ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
				}
			};

			buttonGridLayout.Children.Add(skipButton, 0, 0);
			buttonGridLayout.Children.Add(acceptButton, 1, 0);
			return buttonGridLayout;
		}

		Button createButton(string text, string commandString)
		{
			var button = new Button {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HeightRequest = _buttonHeight,
				CornerRadius = (int)_buttonHeight / 2,
				BackgroundColor = Color.FromHex(Theme.Current.AppStatusBarBackgroundColor),
				TextColor = Color.FromHex(Theme.Current.TestPassingButtonTextColor),
				Text = text
			};

			button.SetBinding(Button.CommandProperty, commandString);
			return button;
		}

		ListView createQuestionList()
		{
			var titleLayout = createTitleLayout();

			var listView = new ListView {
				BackgroundColor = Color.FromHex(Theme.Current.AppBackgroundColor),
				Header = titleLayout,
				HasUnevenRows = true,
				ItemTemplate = new TestingAnswerDataTemplateSelector {
					SingleTemplate = new DataTemplate(typeof(TestingSingleAnswerViewCell)),
					EditableTemplate = new DataTemplate(typeof(TestingEditableAnswerViewCell)),
					MultipleTemplate = new DataTemplate(typeof(TestingMultipleAnswerViewCell)),
					MovableTemplate = new DataTemplate(typeof(TestingMovableAnswerViewCell))
				},
				SeparatorColor = Color.FromHex(Theme.Current.AppBackgroundColor),
				SeparatorVisibility = SeparatorVisibility.None
			};

			listView.ItemTapped += (sender, args) => ((ListView)sender).SelectedItem = null;
			listView.SetBinding(ListView.SelectedItemProperty, "SelectedItem");
			listView.SetBinding(ItemsView<Cell>.ItemsSourceProperty, "Answers");
			return listView;
		}

		StackLayout createTitleLayout()
		{
			var questionLabel = createQuestionLabel();
			var descriptionLabel = createDescriptionLabel();
			return new StackLayout {
				Padding = new Thickness(20),
				Children = {
					questionLabel,
					descriptionLabel
				}
			};
		}

		Label createQuestionLabel()
		{
			var questionLabel = new Label {
				FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
			};

			questionLabel.SetBinding(Label.TextProperty, "Question");
			return questionLabel;
		}

		Label createDescriptionLabel()
		{
			var descriptionLabel = new Label {
				TextType = TextType.Html
			};

			descriptionLabel.SetBinding(Label.TextProperty, "Description");
			return descriptionLabel;
		}
	}
}

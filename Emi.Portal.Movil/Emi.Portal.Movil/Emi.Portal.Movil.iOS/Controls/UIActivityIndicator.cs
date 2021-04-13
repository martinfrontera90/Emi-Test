namespace Emi.Portal.Movil.iOS.Controls
{
    using System;
    using CoreGraphics;
    using UIKit;

    public class UIActivityIndicator : UIView
    {
        // control declarations
        UIActivityIndicatorView activitySpinner;
        UILabel loadingLabel;

        public UIActivityIndicator(CGRect frame)
            : base(frame)
        {
            // configurable bits
            BackgroundColor = UIColor.FromRGBA(1, 1, 1, 0.7f);
            //Alpha = 0.60f;
            AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;

            nfloat labelHeight = 22;
            nfloat labelWidth = Frame.Width - 20;

            // derive the center x and y
            nfloat centerX = Frame.Width / 2;
            nfloat centerY = Frame.Height / 2;

            // create the activity spinner, center it horizontall and put it 5 points above center x
            activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
            activitySpinner.Color = UIColor.Black;
            activitySpinner.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
            activitySpinner.Frame = new CGRect(centerX - (activitySpinner.Frame.Width / 2),
               centerY - activitySpinner.Frame.Height - 20,
               activitySpinner.Frame.Width,
               activitySpinner.Frame.Height);
            AddSubview(activitySpinner);
            activitySpinner.StartAnimating();

            // create and configure the "Loading Data" label
            loadingLabel = new UILabel(new CGRect(
                centerX - (labelWidth / 2),
                centerY + 20,
                labelWidth,
                labelHeight
            ));
            loadingLabel.BackgroundColor = UIColor.Clear;
            loadingLabel.TextColor = UIColor.Black;
            loadingLabel.Text = "Por favor espera...";
            loadingLabel.TextAlignment = UITextAlignment.Center;
            loadingLabel.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
            AddSubview(loadingLabel);
        }

        /// <summary>
        /// Fades out the control and then removes it from the super view
        /// </summary>
        public void Hide()
        {
            UIView.Animate(
                0.3, // duration
                () => { Alpha = 0; },
                () => { RemoveFromSuperview(); }
            );
        }
    }
}
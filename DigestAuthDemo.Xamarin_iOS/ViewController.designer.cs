// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace DigestAuthDemo
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UILabel Txt { get; set; }

		[Action ("OnClearClecked:")]
		partial void OnClearClecked (Foundation.NSObject sender);

		[Action ("OnHttpClientClicked:")]
		partial void OnHttpClientClicked (Foundation.NSObject sender);

		[Action ("OnNsUrlSessionClicked:")]
		partial void OnNsUrlSessionClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (Txt != null) {
				Txt.Dispose ();
				Txt = null;
			}
		}
	}
}

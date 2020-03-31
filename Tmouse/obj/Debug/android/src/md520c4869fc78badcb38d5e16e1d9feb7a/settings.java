package md520c4869fc78badcb38d5e16e1d9feb7a;


public class settings
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Tmouse.settings, Tmouse, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", settings.class, __md_methods);
	}


	public settings () throws java.lang.Throwable
	{
		super ();
		if (getClass () == settings.class)
			mono.android.TypeManager.Activate ("Tmouse.settings, Tmouse, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}

package md56dd882e6687e72a15a6eaa8f9900198e;


public class CustomerWrapper
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("SushEat.Droid.CustomerWrapper, SushEat.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CustomerWrapper.class, __md_methods);
	}


	public CustomerWrapper ()
	{
		super ();
		if (getClass () == CustomerWrapper.class)
			mono.android.TypeManager.Activate ("SushEat.Droid.CustomerWrapper, SushEat.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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

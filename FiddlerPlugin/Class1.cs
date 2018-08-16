using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fiddler;
using FiddlerPlugin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Inspector : IResponseInspector2
{
    public void Clear()
    {
        //throw new NotImplementedException();
    }

    public byte[] body { get; set; }
    public bool bDirty { get; }
    public bool bReadOnly { get; set; }
    public HTTPResponseHeaders headers { get; set; }
}

public class WebViewer : Inspector2, IResponseInspector2
{
    private IResponseInspector2 _responseInspector2Implementation = new Inspector();
    private TestView view = new TestView();
    public override void AddToTab(TabPage o)
    {
        o.Controls.Add(view);
        o.Text = "WarshipGirls";
        //throw new NotImplementedException();
    }

    public override int GetOrder()
    {
        return 0;
        //throw new NotImplementedException();
    }


    public void Viewers()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void Clear()
    {
        _responseInspector2Implementation.Clear();
    }

    public byte[] body
    {
        get { return _responseInspector2Implementation.body; }
        set
        {       
            _responseInspector2Implementation.body = value;
            try
            {
                JObject jObj;
                using (var memstream = new MemoryStream(value))
                {
                    memstream.ReadByte();
                    memstream.ReadByte();
                    using (var dzip = new DeflateStream(memstream, CompressionMode.Decompress))
                    {
                        using (var sr = new StreamReader(dzip))
                        {
                            jObj = JObject.Parse(sr.ReadToEnd());
                        }
                    }
                    view.setText(jObj.ToString());
                }
            }
            catch (Exception e)
            {
                view.setText(e.Message);
            }
        }
    }

    public bool bDirty
    {
        get { return _responseInspector2Implementation.bDirty; }
    }

    public bool bReadOnly
    {
        get { return _responseInspector2Implementation.bReadOnly; }
        set { _responseInspector2Implementation.bReadOnly = value; }
    }

    public HTTPResponseHeaders headers
    {
        get { return _responseInspector2Implementation.headers; }
        set { _responseInspector2Implementation.headers = value; }
    }
}

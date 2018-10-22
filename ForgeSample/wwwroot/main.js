
var viewerApp;
var token;

$.ajax({
    url:"https://forgedemoapp.herokuapp.com/api/forge/oauth/token",
    type: "GET",    
    async: false,   
    success: function (data){
        console.log(data);                         
        token=data.access_token;
        LoadData();
    },
    error:function(err){
        console.log(err);
    }
});

function LoadData(){
    var options = {
        env: 'AutodeskProduction',
        getAccessToken: function(onGetAccessToken) {
            //
            // TODO: Replace static access token string below with call to fetch new token from your backend
            // Both values are provided by Forge's Authentication (OAuth) API.
            //
            // Example Forge's Authentication (OAuth) API return value:
            // {
            //    "access_token": "<YOUR_APPLICATION_TOKEN>",
            //    "token_type": "Bearer",
            //    "expires_in": 86400
            // }
            //
            // var accessToken = 'eyJhbGciOiJIUzI1NiIsImtpZCI6Imp3dF9zeW1tZXRyaWNfa2V5In0.eyJjbGllbnRfaWQiOiJPazIyWnNOTUc3ZUNoQW5Bc2JPSjltM3VQQXZJSk9FSCIsImV4cCI6MTUzOTA2OTAzNCwic2NvcGUiOlsiZGF0YTpyZWFkIl0sImF1ZCI6Imh0dHBzOi8vYXV0b2Rlc2suY29tL2F1ZC9qd3RleHA2MCIsImp0aSI6IjhITWJ5QzNMNFJHYU4wMkx1d0h4VEU4c211bkVSS1p0bU1adnA0SmNHQUNmUjRwcGtubTFOdXllaUNYdlRDbXIifQ.hAxjz-d8im17HRtPs_Nv3v4E4cv59GnSjAM90NRs-bI';
            var expireTimeSeconds = 60 * 30;
            onGetAccessToken(token, expireTimeSeconds);
        }
    };
    
    var documentId = 'urn:dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6bW9kZWwyMDE4LTEwLTEwLTA0LTEzLTI1LWQ0MWQ4Y2Q5OGYwMGIyMDRlOTgwMDk5OGVjZjg0MjdlL0FDTS1QMjgyLUItQUZTVC1OLnJ2dA';
    Autodesk.Viewing.Initializer(options, function onInitialized(){
        viewerApp = new Autodesk.Viewing.ViewingApplication('MyViewerDiv');
    
        var config3d = {
              extensions: ['MyAwesomeExtension']
        };
        viewerApp.registerViewer(viewerApp.k3D, Autodesk.Viewing.Private.GuiViewer3D, config3d);
    
        viewerApp.loadDocument(documentId, onDocumentLoadSuccess, onDocumentLoadFailure);
    });
}


function onDocumentLoadSuccess(doc) {

    // We could still make use of Document.getSubItemsWithProperties()
    // However, when using a ViewingApplication, we have access to the **bubble** attribute,
    // which references the root node of a graph that wraps each object from the Manifest JSON.
    var viewables = viewerApp.bubble.search({'type':'geometry'});
    if (viewables.length === 0) {
        console.error('Document contains no viewables.');
        return;
    }

    // Choose any of the avialble viewables
    viewerApp.selectItem(viewables[0].data, onItemLoadSuccess, onItemLoadFail);
}

function onDocumentLoadFailure(viewerErrorCode) {
    console.error('onDocumentLoadFailure() - errorCode:' + viewerErrorCode);
}

function onItemLoadSuccess(viewer, item) {
    console.log('onItemLoadSuccess()!');
    console.log(viewer);
    console.log(item);

    // Congratulations! The viewer is now ready to be used.
    console.log('Viewers are equal: ' + (viewer === viewerApp.getCurrentViewer()));
}

function onItemLoadFail(errorCode) {
    console.error('onItemLoadFail() - errorCode:' + errorCode);
}


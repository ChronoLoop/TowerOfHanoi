//https://amalgamatelabs.com/Blog/4/data_persistence
var HandleIO = {
    SyncFiles : function()
    {
        FS.syncfs(false,function (err) {
            // handle callback
        });
    }
};

mergeInto(LibraryManager.library, HandleIO);
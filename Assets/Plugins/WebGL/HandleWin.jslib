var HandleWin = {
    SendScores : function(level, moves, time)
    {
        if (typeof window.handleWin !== "undefined"){
            window.handleWin(level, moves, time);
        }
    }
};

mergeInto(LibraryManager.library, HandleWin);
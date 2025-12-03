using System;


namespace AutoUml {
    public enum OpenIconicKind {
        /// <summary>OpenIconic account-login</summary>
        AccountLogin = 0,
        /// <summary>OpenIconic account-logout</summary>
        AccountLogout = 1,
        /// <summary>OpenIconic action-redo</summary>
        ActionRedo = 2,
        /// <summary>OpenIconic action-undo</summary>
        ActionUndo = 3,
        /// <summary>OpenIconic align-center</summary>
        AlignCenter = 4,
        /// <summary>OpenIconic align-left</summary>
        AlignLeft = 5,
        /// <summary>OpenIconic align-right</summary>
        AlignRight = 6,
        /// <summary>OpenIconic aperture</summary>
        Aperture = 7,
        /// <summary>OpenIconic arrow-bottom</summary>
        ArrowBottom = 8,
        /// <summary>OpenIconic arrow-circle-bottom</summary>
        ArrowCircleBottom = 9,
        /// <summary>OpenIconic arrow-circle-left</summary>
        ArrowCircleLeft = 10,
        /// <summary>OpenIconic arrow-circle-right</summary>
        ArrowCircleRight = 11,
        /// <summary>OpenIconic arrow-circle-top</summary>
        ArrowCircleTop = 12,
        /// <summary>OpenIconic arrow-left</summary>
        ArrowLeft = 13,
        /// <summary>OpenIconic arrow-right</summary>
        ArrowRight = 14,
        /// <summary>OpenIconic arrow-thick-bottom</summary>
        ArrowThickBottom = 15,
        /// <summary>OpenIconic arrow-thick-left</summary>
        ArrowThickLeft = 16,
        /// <summary>OpenIconic arrow-thick-right</summary>
        ArrowThickRight = 17,
        /// <summary>OpenIconic arrow-thick-top</summary>
        ArrowThickTop = 18,
        /// <summary>OpenIconic arrow-top</summary>
        ArrowTop = 19,
        /// <summary>OpenIconic audio</summary>
        Audio = 20,
        /// <summary>OpenIconic audio-spectrum</summary>
        AudioSpectrum = 21,
        /// <summary>OpenIconic badge</summary>
        Badge = 22,
        /// <summary>OpenIconic ban</summary>
        Ban = 23,
        /// <summary>OpenIconic bar-chart</summary>
        BarChart = 24,
        /// <summary>OpenIconic basket</summary>
        Basket = 25,
        /// <summary>OpenIconic battery-empty</summary>
        BatteryEmpty = 26,
        /// <summary>OpenIconic battery-full</summary>
        BatteryFull = 27,
        /// <summary>OpenIconic beaker</summary>
        Beaker = 28,
        /// <summary>OpenIconic bell</summary>
        Bell = 29,
        /// <summary>OpenIconic bluetooth</summary>
        Bluetooth = 30,
        /// <summary>OpenIconic bold</summary>
        Bold = 31,
        /// <summary>OpenIconic bolt</summary>
        Bolt = 32,
        /// <summary>OpenIconic book</summary>
        Book = 33,
        /// <summary>OpenIconic bookmark</summary>
        Bookmark = 34,
        /// <summary>OpenIconic box</summary>
        Box = 35,
        /// <summary>OpenIconic briefcase</summary>
        Briefcase = 36,
        /// <summary>OpenIconic british-pound</summary>
        BritishPound = 37,
        /// <summary>OpenIconic browser</summary>
        Browser = 38,
        /// <summary>OpenIconic brush</summary>
        Brush = 39,
        /// <summary>OpenIconic bug</summary>
        Bug = 40,
        /// <summary>OpenIconic bullhorn</summary>
        Bullhorn = 41,
        /// <summary>OpenIconic calculator</summary>
        Calculator = 42,
        /// <summary>OpenIconic calendar</summary>
        Calendar = 43,
        /// <summary>OpenIconic camera-slr</summary>
        CameraSlr = 44,
        /// <summary>OpenIconic caret-bottom</summary>
        CaretBottom = 45,
        /// <summary>OpenIconic caret-left</summary>
        CaretLeft = 46,
        /// <summary>OpenIconic caret-right</summary>
        CaretRight = 47,
        /// <summary>OpenIconic caret-top</summary>
        CaretTop = 48,
        /// <summary>OpenIconic cart</summary>
        Cart = 49,
        /// <summary>OpenIconic chat</summary>
        Chat = 50,
        /// <summary>OpenIconic check</summary>
        Check = 51,
        /// <summary>OpenIconic chevron-bottom</summary>
        ChevronBottom = 52,
        /// <summary>OpenIconic chevron-left</summary>
        ChevronLeft = 53,
        /// <summary>OpenIconic chevron-right</summary>
        ChevronRight = 54,
        /// <summary>OpenIconic chevron-top</summary>
        ChevronTop = 55,
        /// <summary>OpenIconic circle-check</summary>
        CircleCheck = 56,
        /// <summary>OpenIconic circle-x</summary>
        CircleX = 57,
        /// <summary>OpenIconic clipboard</summary>
        Clipboard = 58,
        /// <summary>OpenIconic clock</summary>
        Clock = 59,
        /// <summary>OpenIconic cloud</summary>
        Cloud = 60,
        /// <summary>OpenIconic cloud-download</summary>
        CloudDownload = 61,
        /// <summary>OpenIconic cloud-upload</summary>
        CloudUpload = 62,
        /// <summary>OpenIconic cloudy</summary>
        Cloudy = 63,
        /// <summary>OpenIconic code</summary>
        Code = 64,
        /// <summary>OpenIconic cog</summary>
        Cog = 65,
        /// <summary>OpenIconic collapse-down</summary>
        CollapseDown = 66,
        /// <summary>OpenIconic collapse-left</summary>
        CollapseLeft = 67,
        /// <summary>OpenIconic collapse-right</summary>
        CollapseRight = 68,
        /// <summary>OpenIconic collapse-up</summary>
        CollapseUp = 69,
        /// <summary>OpenIconic command</summary>
        Command = 70,
        /// <summary>OpenIconic comment-square</summary>
        CommentSquare = 71,
        /// <summary>OpenIconic compass</summary>
        Compass = 72,
        /// <summary>OpenIconic contrast</summary>
        Contrast = 73,
        /// <summary>OpenIconic copywriting</summary>
        Copywriting = 74,
        /// <summary>OpenIconic credit-card</summary>
        CreditCard = 75,
        /// <summary>OpenIconic crop</summary>
        Crop = 76,
        /// <summary>OpenIconic dashboard</summary>
        Dashboard = 77,
        /// <summary>OpenIconic data-transfer-download</summary>
        DataTransferDownload = 78,
        /// <summary>OpenIconic data-transfer-upload</summary>
        DataTransferUpload = 79,
        /// <summary>OpenIconic delete</summary>
        Delete = 80,
        /// <summary>OpenIconic dial</summary>
        Dial = 81,
        /// <summary>OpenIconic document</summary>
        Document = 82,
        /// <summary>OpenIconic dollar</summary>
        Dollar = 83,
        /// <summary>OpenIconic double-quote-sans-left</summary>
        DoubleQuoteSansLeft = 84,
        /// <summary>OpenIconic double-quote-sans-right</summary>
        DoubleQuoteSansRight = 85,
        /// <summary>OpenIconic double-quote-serif-left</summary>
        DoubleQuoteSerifLeft = 86,
        /// <summary>OpenIconic double-quote-serif-right</summary>
        DoubleQuoteSerifRight = 87,
        /// <summary>OpenIconic droplet</summary>
        Droplet = 88,
        /// <summary>OpenIconic eject</summary>
        Eject = 89,
        /// <summary>OpenIconic elevator</summary>
        Elevator = 90,
        /// <summary>OpenIconic ellipses</summary>
        Ellipses = 91,
        /// <summary>OpenIconic envelope-closed</summary>
        EnvelopeClosed = 92,
        /// <summary>OpenIconic envelope-open</summary>
        EnvelopeOpen = 93,
        /// <summary>OpenIconic euro</summary>
        Euro = 94,
        /// <summary>OpenIconic excerpt</summary>
        Excerpt = 95,
        /// <summary>OpenIconic expand-down</summary>
        ExpandDown = 96,
        /// <summary>OpenIconic expand-left</summary>
        ExpandLeft = 97,
        /// <summary>OpenIconic expand-right</summary>
        ExpandRight = 98,
        /// <summary>OpenIconic expand-up</summary>
        ExpandUp = 99,
        /// <summary>OpenIconic external-link</summary>
        ExternalLink = 100,
        /// <summary>OpenIconic eye</summary>
        Eye = 101,
        /// <summary>OpenIconic eyedropper</summary>
        Eyedropper = 102,
        /// <summary>OpenIconic file</summary>
        File = 103,
        /// <summary>OpenIconic fire</summary>
        Fire = 104,
        /// <summary>OpenIconic flag</summary>
        Flag = 105,
        /// <summary>OpenIconic flash</summary>
        Flash = 106,
        /// <summary>OpenIconic folder</summary>
        Folder = 107,
        /// <summary>OpenIconic fork</summary>
        Fork = 108,
        /// <summary>OpenIconic fullscreen-enter</summary>
        FullscreenEnter = 109,
        /// <summary>OpenIconic fullscreen-exit</summary>
        FullscreenExit = 110,
        /// <summary>OpenIconic globe</summary>
        Globe = 111,
        /// <summary>OpenIconic graph</summary>
        Graph = 112,
        /// <summary>OpenIconic grid-four-up</summary>
        GridFourUp = 113,
        /// <summary>OpenIconic grid-three-up</summary>
        GridThreeUp = 114,
        /// <summary>OpenIconic grid-two-up</summary>
        GridTwoUp = 115,
        /// <summary>OpenIconic hard-drive</summary>
        HardDrive = 116,
        /// <summary>OpenIconic header</summary>
        Header = 117,
        /// <summary>OpenIconic headphones</summary>
        Headphones = 118,
        /// <summary>OpenIconic heart</summary>
        Heart = 119,
        /// <summary>OpenIconic home</summary>
        Home = 120,
        /// <summary>OpenIconic image</summary>
        Image = 121,
        /// <summary>OpenIconic inbox</summary>
        Inbox = 122,
        /// <summary>OpenIconic infinity</summary>
        Infinity = 123,
        /// <summary>OpenIconic info</summary>
        Info = 124,
        /// <summary>OpenIconic italic</summary>
        Italic = 125,
        /// <summary>OpenIconic justify-center</summary>
        JustifyCenter = 126,
        /// <summary>OpenIconic justify-left</summary>
        JustifyLeft = 127,
        /// <summary>OpenIconic justify-right</summary>
        JustifyRight = 128,
        /// <summary>OpenIconic key</summary>
        Key = 129,
        /// <summary>OpenIconic laptop</summary>
        Laptop = 130,
        /// <summary>OpenIconic layers</summary>
        Layers = 131,
        /// <summary>OpenIconic lightbulb</summary>
        Lightbulb = 132,
        /// <summary>OpenIconic link-broken</summary>
        LinkBroken = 133,
        /// <summary>OpenIconic link-intact</summary>
        LinkIntact = 134,
        /// <summary>OpenIconic list</summary>
        List = 135,
        /// <summary>OpenIconic list-rich</summary>
        ListRich = 136,
        /// <summary>OpenIconic location</summary>
        Location = 137,
        /// <summary>OpenIconic lock-locked</summary>
        LockLocked = 138,
        /// <summary>OpenIconic lock-unlocked</summary>
        LockUnlocked = 139,
        /// <summary>OpenIconic loop</summary>
        Loop = 140,
        /// <summary>OpenIconic loop-circular</summary>
        LoopCircular = 141,
        /// <summary>OpenIconic loop-square</summary>
        LoopSquare = 142,
        /// <summary>OpenIconic magnifying-glass</summary>
        MagnifyingGlass = 143,
        /// <summary>OpenIconic map</summary>
        Map = 144,
        /// <summary>OpenIconic map-marker</summary>
        MapMarker = 145,
        /// <summary>OpenIconic media-pause</summary>
        MediaPause = 146,
        /// <summary>OpenIconic media-play</summary>
        MediaPlay = 147,
        /// <summary>OpenIconic media-record</summary>
        MediaRecord = 148,
        /// <summary>OpenIconic media-skip-backward</summary>
        MediaSkipBackward = 149,
        /// <summary>OpenIconic media-skip-forward</summary>
        MediaSkipForward = 150,
        /// <summary>OpenIconic media-step-backward</summary>
        MediaStepBackward = 151,
        /// <summary>OpenIconic media-step-forward</summary>
        MediaStepForward = 152,
        /// <summary>OpenIconic media-stop</summary>
        MediaStop = 153,
        /// <summary>OpenIconic medical-cross</summary>
        MedicalCross = 154,
        /// <summary>OpenIconic menu</summary>
        Menu = 155,
        /// <summary>OpenIconic microphone</summary>
        Microphone = 156,
        /// <summary>OpenIconic minus</summary>
        Minus = 157,
        /// <summary>OpenIconic monitor</summary>
        Monitor = 158,
        /// <summary>OpenIconic moon</summary>
        Moon = 159,
        /// <summary>OpenIconic move</summary>
        Move = 160,
        /// <summary>OpenIconic musical-note</summary>
        MusicalNote = 161,
        /// <summary>OpenIconic paperclip</summary>
        Paperclip = 162,
        /// <summary>OpenIconic pencil</summary>
        Pencil = 163,
        /// <summary>OpenIconic people</summary>
        People = 164,
        /// <summary>OpenIconic person</summary>
        Person = 165,
        /// <summary>OpenIconic phone</summary>
        Phone = 166,
        /// <summary>OpenIconic pie-chart</summary>
        PieChart = 167,
        /// <summary>OpenIconic pin</summary>
        Pin = 168,
        /// <summary>OpenIconic play-circle</summary>
        PlayCircle = 169,
        /// <summary>OpenIconic plus</summary>
        Plus = 170,
        /// <summary>OpenIconic power-standby</summary>
        PowerStandby = 171,
        /// <summary>OpenIconic print</summary>
        Print = 172,
        /// <summary>OpenIconic project</summary>
        Project = 173,
        /// <summary>OpenIconic pulse</summary>
        Pulse = 174,
        /// <summary>OpenIconic puzzle-piece</summary>
        PuzzlePiece = 175,
        /// <summary>OpenIconic question-mark</summary>
        QuestionMark = 176,
        /// <summary>OpenIconic rain</summary>
        Rain = 177,
        /// <summary>OpenIconic random</summary>
        Random = 178,
        /// <summary>OpenIconic reload</summary>
        Reload = 179,
        /// <summary>OpenIconic resize-both</summary>
        ResizeBoth = 180,
        /// <summary>OpenIconic resize-height</summary>
        ResizeHeight = 181,
        /// <summary>OpenIconic resize-width</summary>
        ResizeWidth = 182,
        /// <summary>OpenIconic rss</summary>
        Rss = 183,
        /// <summary>OpenIconic rss-alt</summary>
        RssAlt = 184,
        /// <summary>OpenIconic script</summary>
        Script = 185,
        /// <summary>OpenIconic share</summary>
        Share = 186,
        /// <summary>OpenIconic share-boxed</summary>
        ShareBoxed = 187,
        /// <summary>OpenIconic shield</summary>
        Shield = 188,
        /// <summary>OpenIconic signal</summary>
        Signal = 189,
        /// <summary>OpenIconic signpost</summary>
        Signpost = 190,
        /// <summary>OpenIconic sort-ascending</summary>
        SortAscending = 191,
        /// <summary>OpenIconic sort-descending</summary>
        SortDescending = 192,
        /// <summary>OpenIconic spreadsheet</summary>
        Spreadsheet = 193,
        /// <summary>OpenIconic star</summary>
        Star = 194,
        /// <summary>OpenIconic sun</summary>
        Sun = 195,
        /// <summary>OpenIconic tablet</summary>
        Tablet = 196,
        /// <summary>OpenIconic tag</summary>
        Tag = 197,
        /// <summary>OpenIconic tags</summary>
        Tags = 198,
        /// <summary>OpenIconic target</summary>
        Target = 199,
        /// <summary>OpenIconic task</summary>
        Task = 200,
        /// <summary>OpenIconic terminal</summary>
        Terminal = 201,
        /// <summary>OpenIconic text</summary>
        Text = 202,
        /// <summary>OpenIconic thumb-down</summary>
        ThumbDown = 203,
        /// <summary>OpenIconic thumb-up</summary>
        ThumbUp = 204,
        /// <summary>OpenIconic timer</summary>
        Timer = 205,
        /// <summary>OpenIconic transfer</summary>
        Transfer = 206,
        /// <summary>OpenIconic trash</summary>
        Trash = 207,
        /// <summary>OpenIconic underline</summary>
        Underline = 208,
        /// <summary>OpenIconic vertical-align-bottom</summary>
        VerticalAlignBottom = 209,
        /// <summary>OpenIconic vertical-align-center</summary>
        VerticalAlignCenter = 210,
        /// <summary>OpenIconic vertical-align-top</summary>
        VerticalAlignTop = 211,
        /// <summary>OpenIconic video</summary>
        Video = 212,
        /// <summary>OpenIconic volume-high</summary>
        VolumeHigh = 213,
        /// <summary>OpenIconic volume-low</summary>
        VolumeLow = 214,
        /// <summary>OpenIconic volume-off</summary>
        VolumeOff = 215,
        /// <summary>OpenIconic warning</summary>
        Warning = 216,
        /// <summary>OpenIconic wifi</summary>
        Wifi = 217,
        /// <summary>OpenIconic wrench</summary>
        Wrench = 218,
        /// <summary>OpenIconic x</summary>
        X = 219,
        /// <summary>OpenIconic yen</summary>
        Yen = 220,
        /// <summary>OpenIconic zoom-in</summary>
        ZoomIn = 221,
        /// <summary>OpenIconic zoom-out</summary>
        ZoomOut = 222
    }

    public static class OpenIconicKindUtils {
        static readonly string[] Names =
        [
            "account-login",
            "account-logout",
            "action-redo",
            "action-undo",
            "align-center",
            "align-left",
            "align-right",
            "aperture",
            "arrow-bottom",
            "arrow-circle-bottom",
            "arrow-circle-left",
            "arrow-circle-right",
            "arrow-circle-top",
            "arrow-left",
            "arrow-right",
            "arrow-thick-bottom",
            "arrow-thick-left",
            "arrow-thick-right",
            "arrow-thick-top",
            "arrow-top",
            "audio",
            "audio-spectrum",
            "badge",
            "ban",
            "bar-chart",
            "basket",
            "battery-empty",
            "battery-full",
            "beaker",
            "bell",
            "bluetooth",
            "bold",
            "bolt",
            "book",
            "bookmark",
            "box",
            "briefcase",
            "british-pound",
            "browser",
            "brush",
            "bug",
            "bullhorn",
            "calculator",
            "calendar",
            "camera-slr",
            "caret-bottom",
            "caret-left",
            "caret-right",
            "caret-top",
            "cart",
            "chat",
            "check",
            "chevron-bottom",
            "chevron-left",
            "chevron-right",
            "chevron-top",
            "circle-check",
            "circle-x",
            "clipboard",
            "clock",
            "cloud",
            "cloud-download",
            "cloud-upload",
            "cloudy",
            "code",
            "cog",
            "collapse-down",
            "collapse-left",
            "collapse-right",
            "collapse-up",
            "command",
            "comment-square",
            "compass",
            "contrast",
            "copywriting",
            "credit-card",
            "crop",
            "dashboard",
            "data-transfer-download",
            "data-transfer-upload",
            "delete",
            "dial",
            "document",
            "dollar",
            "double-quote-sans-left",
            "double-quote-sans-right",
            "double-quote-serif-left",
            "double-quote-serif-right",
            "droplet",
            "eject",
            "elevator",
            "ellipses",
            "envelope-closed",
            "envelope-open",
            "euro",
            "excerpt",
            "expand-down",
            "expand-left",
            "expand-right",
            "expand-up",
            "external-link",
            "eye",
            "eyedropper",
            "file",
            "fire",
            "flag",
            "flash",
            "folder",
            "fork",
            "fullscreen-enter",
            "fullscreen-exit",
            "globe",
            "graph",
            "grid-four-up",
            "grid-three-up",
            "grid-two-up",
            "hard-drive",
            "header",
            "headphones",
            "heart",
            "home",
            "image",
            "inbox",
            "infinity",
            "info",
            "italic",
            "justify-center",
            "justify-left",
            "justify-right",
            "key",
            "laptop",
            "layers",
            "lightbulb",
            "link-broken",
            "link-intact",
            "list",
            "list-rich",
            "location",
            "lock-locked",
            "lock-unlocked",
            "loop",
            "loop-circular",
            "loop-square",
            "magnifying-glass",
            "map",
            "map-marker",
            "media-pause",
            "media-play",
            "media-record",
            "media-skip-backward",
            "media-skip-forward",
            "media-step-backward",
            "media-step-forward",
            "media-stop",
            "medical-cross",
            "menu",
            "microphone",
            "minus",
            "monitor",
            "moon",
            "move",
            "musical-note",
            "paperclip",
            "pencil",
            "people",
            "person",
            "phone",
            "pie-chart",
            "pin",
            "play-circle",
            "plus",
            "power-standby",
            "print",
            "project",
            "pulse",
            "puzzle-piece",
            "question-mark",
            "rain",
            "random",
            "reload",
            "resize-both",
            "resize-height",
            "resize-width",
            "rss",
            "rss-alt",
            "script",
            "share",
            "share-boxed",
            "shield",
            "signal",
            "signpost",
            "sort-ascending",
            "sort-descending",
            "spreadsheet",
            "star",
            "sun",
            "tablet",
            "tag",
            "tags",
            "target",
            "task",
            "terminal",
            "text",
            "thumb-down",
            "thumb-up",
            "timer",
            "transfer",
            "trash",
            "underline",
            "vertical-align-bottom",
            "vertical-align-center",
            "vertical-align-top",
            "video",
            "volume-high",
            "volume-low",
            "volume-off",
            "warning",
            "wifi",
            "wrench",
            "x",
            "yen",
            "zoom-in",
            "zoom-out"
        ];
 
       public static string ToCode(this OpenIconicKind kind)
        {
            var idx = (int)kind;
            if (idx < 0 || idx >= Names.Length)
                throw new ArgumentException(nameof(kind));
            return Names[idx];
        }
    }
}
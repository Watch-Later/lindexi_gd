﻿// See https://aka.ms/new-console-template for more information

using CPF.Linux;
using System;
using System.Diagnostics;
using System.Runtime;
using static CPF.Linux.XLib;

XInitThreads();
var display = XOpenDisplay(IntPtr.Zero);
var screen = XDefaultScreen(display);

var rootWindow = XDefaultRootWindow(display);

XMatchVisualInfo(display, screen, 32, 4, out var info);
var visual = info.visual;

var valueMask =
        //SetWindowValuemask.BackPixmap
        0
        | SetWindowValuemask.BackPixel
        | SetWindowValuemask.BorderPixel
        | SetWindowValuemask.BitGravity
        | SetWindowValuemask.WinGravity
        | SetWindowValuemask.BackingStore
        | SetWindowValuemask.ColorMap
    //| SetWindowValuemask.OverrideRedirect
    ;
var xSetWindowAttributes = new XSetWindowAttributes
{
    backing_store = 1,
    bit_gravity = Gravity.NorthWestGravity,
    win_gravity = Gravity.NorthWestGravity,
    //override_redirect = true, // 设置窗口的override_redirect属性为True，以避免窗口管理器的干预
    colormap = XCreateColormap(display, rootWindow, visual, 0),
    border_pixel = 0,
    background_pixel = 0,
};

var xDisplayWidth = XDisplayWidth(display, screen) / 2;
var xDisplayHeight = XDisplayHeight(display, screen) / 2;
var handle = XCreateWindow(display, rootWindow, 0, 0, xDisplayWidth, xDisplayHeight, 5,
    32,
    (int)CreateWindowArgs.InputOutput,
    visual,
    (nuint)valueMask, ref xSetWindowAttributes);


XEventMask ignoredMask = XEventMask.SubstructureRedirectMask | XEventMask.ResizeRedirectMask |
                         XEventMask.PointerMotionHintMask;
var mask = new IntPtr(0xffffff ^ (int)ignoredMask);
XSelectInput(display, handle, mask);

XMapWindow(display, handle);
XFlush(display);

var white = XWhitePixel(display, screen);
var black = XBlackPixel(display, screen);

var gc = XCreateGC(display, handle, 0, 0);
XSetForeground(display, gc, white);
XSync(display, false);

_ = Task.Run(async () =>
{
    while (true)
    {
        await Task.Delay(TimeSpan.FromSeconds(1));

        var @event = new XEvent
        {
            ClientMessageEvent =
            {
                type = XEventName.ClientMessage,
                send_event = true,
                window = handle,
                message_type = 0,
                format = 32,
                ptr1 = 0,
                ptr2 = 0,
                ptr3 = 0,
                ptr4 = 0,
            }
        };
        XSendEvent(display, handle, false, 0, ref @event);

        XFlush(display);
    }
<<<<<<< HEAD

    //if (long.TryParse(args[1], out var otherProcessGCHandle))
    //{
    //    window2GCHandle = new IntPtr(otherProcessGCHandle);
    //}
    // 不用别人传的，从窗口进行创建
    window2GCHandle = XCreateGC(display, window2Handle, 0, 0);
}

XIDeviceInfo? pointerDevice = default;
unsafe
{
    var devices = (XIDeviceInfo*) XLib.XIQueryDevice(display,
        (int) XiPredefinedDeviceId.XIAllMasterDevices, out int num);

    for (var c = 0; c < num; c++)
    {
        if (devices[c].Use == XiDeviceType.XIMasterPointer)
        {
            pointerDevice = devices[c];
            break;
        }
    }
}

if (pointerDevice != null)
{
    XiEventType[] multiTouchEventTypes =
    [
        XiEventType.XI_TouchBegin,
        XiEventType.XI_TouchUpdate,
        XiEventType.XI_TouchEnd
    ];

    XiEventType[] defaultEventTypes =
    [
        XiEventType.XI_Motion,
        XiEventType.XI_ButtonPress,
        XiEventType.XI_ButtonRelease,
        XiEventType.XI_Leave,
        XiEventType.XI_Enter,
    ];

    List<XiEventType> eventTypes = [.. multiTouchEventTypes, .. defaultEventTypes];

    XiSelectEvents(display, window1.Window, new Dictionary<int, List<XiEventType>> { [pointerDevice.Value.Deviceid] = eventTypes });
}

=======
});
>>>>>>> 33dbc36c47d6f1e68265c0f0f389a566823425fd

while (true)
{
    var xNextEvent = XNextEvent(display, out var @event);
    if (xNextEvent != 0)
    {
        Console.WriteLine($"xNextEvent {xNextEvent}");
        break;
    }

    if (args.Length == 0)
    {
        Console.WriteLine(@event.type);
    }

    if (@event.type == XEventName.Expose)
    {
        XDrawLine(display, handle, gc, 0, 0, 100, 100);
    }
<<<<<<< HEAD
<<<<<<< HEAD
    else if (@event.type == XEventName.MotionNotify)
    {
        var x = @event.MotionEvent.x;
        var y = @event.MotionEvent.y;

        if (window2Handle != 0 && window2GCHandle != 0)
        {
            // 绘制是无效的
            //XDrawLine(display, window2Handle, window2GCHandle, x, y, x + 100, y);

            var xEvent = new XEvent
            {
                MotionEvent =
                {
                    type = XEventName.MotionNotify,
                    send_event = true,
                    window = window2Handle,
                    display = display,
                    x = x,
                    y = y
                }
            };
            XSendEvent(display, window2Handle, propagate: false, new IntPtr((int) (EventMask.ButtonMotionMask)),
                ref xEvent);
        }
        else
        {
            XDrawLine(display, window1.Window, window1.GC, x, y, x + 100, y);
        }

        //if (@event.MotionEvent.window == window1.Window)
        //{
        //    XDrawLine(display, window1.Window, window1.GC, x, y, x + 100, y);
        //}
        //else
        //{
        //    var xEvent = new XEvent
        //    {
        //        MotionEvent =
        //        {
        //            type = XEventName.MotionNotify,
        //            send_event = true,
        //            window = window1.Window,
        //            display = display,
        //            x = x,
        //            y = y
        //        }
        //    };
        //    XSendEvent(display, window1.Window, propagate: false, new IntPtr((int)(EventMask.ButtonMotionMask)),
        //        ref xEvent);
        //}
    }
<<<<<<< HEAD
    else if (@event.type == XEventName.GenericEvent)
    {
        unsafe
        {
            var eventCookie = @event.GenericEventCookie;
            void* data = &eventCookie;
            XGetEventData(display, data);
            try
            {
                var xiEvent = (XIEvent*) eventCookie.data;

                if (xiEvent->evtype is
                     XiEventType.XI_Motion
                    or XiEventType.XI_TouchUpdate)
                {

                    var xiDeviceEvent = (XIDeviceEvent*) xiEvent;

                    var x = (int) xiDeviceEvent->event_x;
                    var y = (int) xiDeviceEvent->event_y;

                    //Console.WriteLine($"copyXIDeviceEvent.RootWindow={xiDeviceEvent->RootWindow} rootWindow={rootWindow}"); // 两个进程是相同的

                    if (window2Handle != 0 && window2GCHandle != 0)
                    {
                        XIDeviceEvent copyXIDeviceEvent = *xiDeviceEvent;
                        copyXIDeviceEvent.EventWindow = window2Handle;

                        //Console.WriteLine($"extension={eventCookie.extension}");

                        var xEvent = new XEvent
                        {
                            GenericEventCookie =
                            {
                                type = (int) XEventName.GenericEvent,
                                send_event = true,
                                display = display,
                                cookie = eventCookie.cookie,
                                evtype = eventCookie.evtype,
                                //extension = eventCookie.extension, // 设置了会炸掉
                                serial = eventCookie.serial, // Serial number of failed request
                                data = &copyXIDeviceEvent,
                                //data = (void*) 0
                            }
                        };
                        var status = XSendEvent(display, window2Handle, propagate: false, new IntPtr(0),
                             ref xEvent);
                        Console.WriteLine($"SendEvent {status}");
                    }
                    else
                    {
                        XDrawLine(display, window1.Window, window1.GC, x, y, x + 100, y);
                    }
                }
            }
            finally
            {
                XFreeEventData(display, data);
            }
        }
=======

<<<<<<< HEAD
    var count = XEventsQueued(display, 0 /*QueuedAlready*/);
    if (count == 0)
    {
        var result = XIconifyWindow(display, window1.Window, screen);
        Console.WriteLine($"XIconifyWindow {result}");
>>>>>>> 86cbdc30df6681d1a8da8a287f2cdcc44f9e8e8f
    }
=======
    // 这是有用的
    //var count = XEventsQueued(display, 0 /*QueuedAlready*/);
    //if (count == 0)
    //{
    //    var result = XIconifyWindow(display, window1.Window, screen);
    //    Console.WriteLine($"XIconifyWindow {result}");
    //}
>>>>>>> 4824a4bc1e13ba0da4e7d4a67b93a75e12ee99a5
=======
>>>>>>> 33dbc36c47d6f1e68265c0f0f389a566823425fd
=======

    Console.WriteLine(@event.type);
>>>>>>> 442250a21eb41b8fb07dc30c14d07e629fbc452f
}

Console.WriteLine("Hello, World!");
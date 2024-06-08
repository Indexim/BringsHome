var CompCalendar = function() {
    var calendarEvents  = $('.calendar-events');
    var initEvents = function() {
        calendarEvents.find('li').each(function() {
            var eventObject = { title: $.trim($(this).text()), color: $(this).css('background-color') };
            $(this).data('eventObject', eventObject);
            $(this).draggable({ zIndex: 999, revert: true, revertDuration: 0 });
        });
    };

    return {
        init: function() {
            initEvents();
            var eventInput      = $('#add-event');
            var eventInputVal   = '';
            $('#add-event-btn').on('click', function(){
                eventInputVal = eventInput.prop('value');
                if ( eventInputVal ) {
                    calendarEvents.prepend('<li class="animation-fadeInQuick2Inv"><i class="fa fa-calendar"></i> ' + $('<div />').text(eventInputVal).html() + '</li>');
                    eventInput.prop('value', '');
                    initEvents();
                    eventInput.focus();
                }
                return false;
            });
            var date = new Date();
            var d = date.getDate();
            var m = date.getMonth();
            var y = date.getFullYear();

            $('#calendar').fullCalendar({
                header: {
                    left: 'title',
                    center: '',
                    right: 'today month,agendaWeek,agendaDay prev,next'
                },
                firstDay: 1,
                editable: true,
                droppable: true,
                drop: function(date, allDay) {
                    var originalEventObject = $(this).data('eventObject');
                    var copiedEventObject = $.extend({}, originalEventObject);
                    copiedEventObject.start = date;
                    $('#calendar').fullCalendar('renderEvent', copiedEventObject, true);
                    $(this).remove();
                },
                events: [
                    {
                        title: 'DINAS',
                        start: new Date(y, m, 3),
                        end: new Date(y, m, 6),
                        allDay: true,
                        color: '#7fffd4',
                        rendering: 'background'
                    },
                    {
                        title: 'ROSTER',
                        start: new Date(y, m, 14),
                        end: new Date(y, m, 14),
                        allDay: true,
                        color: '#7fffd4',
                        rendering: 'background'
                    },
                    {
                        title: 'OFF',
                        start: new Date(y, m, 24),
                        end: new Date(y, m, 27),
                        allDay: true,
                        color: '#FFD700'
                    },
                    {
                        title: 'Follow me on Twitter',
                        start: new Date(y, m, 23),
                        end: new Date(y, m, 23),
                        allDay: true,
                        //url: 'http://twitter.com/pixelcave'
                    },
                ],
            });
        }
    };
}();
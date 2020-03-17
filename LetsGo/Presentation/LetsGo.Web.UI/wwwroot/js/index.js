// canvas._objects
let canvas
let number
const grid = 30
const backgroundColor = '#f8f8f8'
const lineStroke = '#ebebeb'
const tableFill = 'rgba(0, 0, 0, 0.7)'
const tableStroke = '#694d23'
const tableShadow = 'rgba(0, 0, 0, 0.4) 3px 3px 7px'

let widthEl = document.getElementById('width')
let heightEl = document.getElementById('height')
let canvasEl = document.getElementById('canvas')

function initCanvas() {
    if (canvas) {
        canvas.clear()
        canvas.dispose()
    }

    canvas = new fabric.Canvas('canvas')
    number = 1
    canvas.backgroundColor = backgroundColor

    for (let i = 0; i < (canvas.height / grid); i++) {
        const lineX = new fabric.Line([0, i * grid, canvas.height, i * grid], {
            stroke: lineStroke,
            selectable: false,
            type: 'line'
        })
        const lineY = new fabric.Line([i * grid, 0, i * grid, canvas.height], {
            stroke: lineStroke,
            selectable: false,
            type: 'line'
        })
        sendLinesToBack()
        canvas.add(lineX)
        canvas.add(lineY)
    }

    canvas.on('object:moving', function (e) {
        snapToGrid(e.target)
    })

    canvas.on('object:scaling', function (e) {
        if (e.target.scaleX > 5) {
            e.target.scaleX = 5
        }
        if (e.target.scaleY > 5) {
            e.target.scaleY = 5
        }
        if (!e.target.strokeWidthUnscaled && e.target.strokeWidth) {
            e.target.strokeWidthUnscaled = e.target.strokeWidth
        }
        if (e.target.strokeWidthUnscaled) {
            e.target.strokeWidth = e.target.strokeWidthUnscaled / e.target.scaleX
            if (e.target.strokeWidth === e.target.strokeWidthUnscaled) {
                e.target.strokeWidth = e.target.strokeWidthUnscaled / e.target.scaleY
            }
        }
    })

    canvas.on('object:modified', function (e) {
        e.target.scaleX = e.target.scaleX >= 0.25 ? (Math.round(e.target.scaleX * 2) / 2) : 0.5
        e.target.scaleY = e.target.scaleY >= 0.25 ? (Math.round(e.target.scaleY * 2) / 2) : 0.5
        snapToGrid(e.target)
        if (e.target.type === 'table') {
            canvas.bringToFront(e.target)
        }
        else {
            canvas.sendToBack(e.target)
        }
        sendLinesToBack()
    })

    canvas.observe('object:moving', function (e) {
        checkBoudningBox(e)
    })
    canvas.observe('object:rotating', function (e) {
        checkBoudningBox(e)
    })
    canvas.observe('object:scaling', function (e) {
        checkBoudningBox(e)
    })
}
initCanvas()

function resizeCanvas() {
    widthEl = document.getElementById('width')
    heightEl = document.getElementById('height')
    canvasEl.width = widthEl.value ? widthEl.value : 812
    canvasEl.height = heightEl.value ? heightEl.value : 812
    const canvasContainerEl = document.querySelectorAll('.canvas-container')[0]
    canvasContainerEl.style.width = canvasEl.width
    canvasContainerEl.style.height = canvasEl.height
}
resizeCanvas()

widthEl.addEventListener('change', () => {
    resizeCanvas()
    initCanvas()
    addDefaultObjects()
})
heightEl.addEventListener('change', () => {
    resizeCanvas()
    initCanvas()
    addDefaultObjects()
})

function generateId() {
    return Math.random().toString(36).substr(2, 8)
}

function addRect(left, top, width, height) {
    const id = generateId()
    const o = new fabric.Rect({
        width: width,
        height: height,
        fill: tableFill,
        stroke: tableStroke,
        strokeWidth: 2,
        shadow: tableShadow,
        originX: 'center',
        originY: 'center',
        centeredRotation: true,
        snapAngle: 45,
        selectable: true,
    })
    const t = new fabric.IText(number.toString(), {
        fontFamily: 'Calibri',
        fontSize: 14,
        fill: '#fff',
        textAlign: 'center',
        originX: 'center',
        originY: 'center'
    })
    const g = new fabric.Group([o, t], {
        left: left,
        top: top,
        centeredRotation: true,
        snapAngle: 45,
        selectable: true,
        type: 'table',
        id: id,
        number: number
    })
    canvas.add(g)
    number++
    return g
}

function snapToGrid(target) {
    target.set({
        left: Math.round(target.left / (grid / 2)) * grid / 2,
        top: Math.round(target.top / (grid / 2)) * grid / 2
    })
}

function checkBoudningBox(e) {
    const obj = e.target

    if (!obj) {
        return
    }
    obj.setCoords()

    const objBoundingBox = obj.getBoundingRect()
    if (objBoundingBox.top < 0) {
        obj.set('top', 0)
        obj.setCoords()
    }
    if (objBoundingBox.left > canvas.width - objBoundingBox.width) {
        obj.set('left', canvas.width - objBoundingBox.width)
        obj.setCoords()
    }
    if (objBoundingBox.top > canvas.height - objBoundingBox.height) {
        obj.set('top', canvas.height - objBoundingBox.height)
        obj.setCoords()
    }
    if (objBoundingBox.left < 0) {
        obj.set('left', 0)
        obj.setCoords()
    }
}

function sendLinesToBack() {
    canvas.getObjects().map(o => {
        if (o.type === 'line') {
            canvas.sendToBack(o)
        }
    })
}

document.querySelectorAll('.rectangle')[0].addEventListener('click', function () {
    const o = addRect(0, 0, 60, 60)
    canvas.setActiveObject(o)
})

document.querySelectorAll('.chair')[0].addEventListener('click', function () {
    const o = addChair(0, 0)
    canvas.setActiveObject(o)
})

document.querySelectorAll('.remove')[0].addEventListener('click', function () {
    console.log('CLICOU 1');
    const o = canvas.getActiveObject()
    if (o) {
        console.log('CLICOU 2');
        o.remove()
        canvas.remove(o)
        canvas.discardActiveObject()
        canvas.renderAll()
    }
})

function formatTime(val) {
    const hours = Math.floor(val / 60)
    const minutes = val % 60
    const englishHours = hours > 12 ? hours - 12 : hours

    const normal = hours + ':' + minutes + (minutes === 0 ? '0' : '')
    const english = englishHours + ':' + minutes + (minutes === 0 ? '0' : '') + ' ' + (hours > 12 ? 'PM' : 'AM')

    return normal + ' (' + english + ')'
}

document.querySelectorAll('.submit')[0].addEventListener('click', function () {
    const obj = canvas.getActiveObject()
    $('#modal').modal('show')
    let modalText = 'You have not selected anything'
    if (obj) {
        modalText = 'You have selected table ' + obj.number + ', time: ' + formatTime(slider.noUiSlider.get())
    }
    document.querySelectorAll('#modal-table-id')[0].innerHTML = modalText
})

const slider = document.getElementById('slider')
noUiSlider.create(slider, {
    start: 1200,
    step: 15,
    connect: 'lower',
    range: {
        min: 0,
        max: 1425
    }
})

const sliderValue = document.getElementById('slider-value')
slider.noUiSlider.on('update', function (values, handle) {
    sliderValue.innerHTML = formatTime(values[handle])
})

function addDefaultObjects() {
    //addChair(225, 75)
    //addChair(255, 75)
    //addChair(225, 135)
    //addChair(255, 135)
    //addChair(225, 195)
    //addChair(255, 195)
    //addChair(225, 255)
    //addChair(255, 255)
    //addChair(15, 195)
    //addChair(45, 195)
    //addChair(15, 255)
    //addChair(45, 255)
    //addChair(15, 315)
    //addChair(45, 315)
    //addChair(15, 375)
    //addChair(45, 375)
    //addChair(225, 315)
    //addChair(255, 315)
    //addChair(225, 375)
    //addChair(255, 375)
    //addChair(15, 435)
    //addChair(15, 495)
    //addChair(15, 555)
    //addChair(15, 615)
    //addChair(225, 615)
    //addChair(255, 615)
    //addChair(195, 495)
    //addChair(195, 525)
    //addChair(255, 495)
    //addChair(255, 525)
    //addChair(225, 675)
    //addChair(255, 675)

    addRect(30, 90, 60, 90)
    //addRect(210, 90, 90, 60)
    //addRect(210, 210, 90, 60)
    //addRect(0, 210, 90, 60)
    //addRect(0, 330, 90, 60)
    //addRect(210, 330, 90, 60)
    //addRect(0, 450, 60, 60)
    //addRect(0, 570, 60, 60)
    //addRect(210, 480, 60, 90)
    //addRect(210, 630, 90, 60)
}

function UpdateBookState() {
    console.log('AQUI');
}

window.addEventListener("load", function (event) {
    addDefaultObjects()
    console.log("Olá");
    console.log(this.document);

    $.ajax({
        type: "GET",
        url: '/Home/GetTableState/1',
        dataType: 'text',
        success: function (result) {
            if (result == 0) {
                console.log('IGUAL')
            }
            else {
                console.log('DIFERENTE')
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr);
            console.log(thrownError);
        }
    });

});




//$.ajax({
//    type: "POST",
//    url: '/Home/GetTableState',
//    dataType: 'json',
//    data: { usuarioId: $("#CodUsuario").val(), hoteis: hoteisAcesso },
//    success: function (result) {
//        if (result != null) {
//            if (result.Data[0].length > 0 && hoteisAcesso != "") {
//                $("#divAcessos").show()
//            }
//            //else {
//            //    $("#divAcessos").hide()
//            //}
//            result.Data[0].forEach(function (item) {
//                opts = opts + '<option value="' + item.Id + '" data-codHotel="' + item.CodHotel + '">' + item.Nome + '</option>'
//            });
//            //if (hoteisAcesso != "") {
//            acessos.forEach(function (itemAcesso) {
//                var removerAcesso = true;
//                console.log(acessos)
//                console.log(result.Data[0])
//                if (result.Data[0].length > 0) {
//                    result.Data[0].forEach(function (itemAcessoHotel) {
//                        if (itemAcesso.Id == itemAcessoHotel.Id || itemAcesso.CodHotel == 0)
//                            removerAcesso = false
//                    })
//                    if (removerAcesso)
//                        $("#btnRemove_" + itemAcesso.Id).click();
//                } else {
//                    $(".btnRemove").click()
//                }
//            })
//            //}
//        }
//        $("#Acesso").html("");
//        $("#Acesso").append(opts);
//    },
//    error: function (xhr, ajaxOptions, thrownError) {
//        console.log(xhr);
//        console.log(thrownError);
//    }

//});
//[Route("GetAcessosByHotel")]
//[HttpPost]
//[ResponseType(typeof (Response<List<Acesso>>))]
//    //[App_Start.CustomActionAuthorize(false)]
//    public HttpResponseMessage GetAcessosByHotel([FromBody] string hoteis)
//        {
//        try
//            {
//            var response = _manager.GetAcessosByHotel(hoteis);
//            return base.ReturnResponseGet(response);
//        }
//            catch(Exception ex) {
//            this.log.Error(ex.Message);
//            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
//        }
//    }


//});
//[HttpGet]
//[ResponseType(typeof (Response<List<Hotel>>))]
//    [Route("GetHoteisByUsuarioId")]
//        public HttpResponseMessage GetHoteisByUsuarioId(int id)
//        {
//        try
//            {
//            var response = _manager.GetHoteisByUsuarioId(id);
//            return base.ReturnResponseGet(response);
//        }
//            catch(Exception ex) {
//            this.log.Error(ex.Message);
//            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Resources.Language.obterHoteisUsuarioError);
//        }
//    }
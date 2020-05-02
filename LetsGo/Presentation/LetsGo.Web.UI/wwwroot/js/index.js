let canvas
let dataUltimaAtualizacao

const grid = 30
const backgroundColor = '#f8f8f8'
const lineStroke = '#ebebeb'
const tableStroke = '#694d23'
const tableShadow = 'rgba(0, 0, 0, 0.4) 3px 3px 7px'
const tableStrokeWidth = 3

const corMesaOcupada = 'rgba(251, 49, 49, 0.95)'
const corMesaVazia = 'rgba(11, 277, 11, 0.95)'
const corMesaNaoMonitorada = 'rgba(243, 243, 11, 0.95)'

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
    canvasEl.width = 812
    canvasEl.height = 812
    const canvasContainerEl = document.querySelectorAll('.canvas-container')[0]
    canvasContainerEl.style.width = canvasEl.width
    canvasContainerEl.style.height = canvasEl.height
}

resizeCanvas()

function generateId() {
    return Math.random().toString(36).substr(2, 8)
}

function addRect(left, top, width, height, color, number) {
    const id = generateId()
    const o = new fabric.Rect({
        width: width,
        height: height,
        fill: color,
        stroke: tableStroke,
        strokeWidth: tableStrokeWidth,
        shadow: tableShadow,
        originX: 'center',
        originY: 'center',
        centeredRotation: true,
        snapAngle: 45,
        selectable: true,
    })
    const t = new fabric.IText(number.toString(), {
        fontFamily: 'Calibri',
        fontSize: 30,
        fill: '#000',
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

function inserirMesa(mesa, index) {

    let corMesa = corMesaNaoMonitorada;

    if (mesa.estado == 1)
        corMesa = corMesaVazia;

    if (mesa.estado == 2)
        corMesa = corMesaOcupada;

    addRect(mesa.coordenada.esquerda, mesa.coordenada.topo, mesa.coordenada.largura, mesa.coordenada.altura, corMesa, mesa.numero);
}

function getMaxDataUltimaAlteracao(mesas) {
    return mesas.reduce((max, item) => item.dataAlteracaoEstado > max ? item.dataAlteracaoEstado : max, mesas[0].dataAlteracaoEstado);
}

function removerMesas() {

    let canvasObjects = canvas.getObjects()

    for (let i = canvasObjects.length - 1; i >= 0; i--) {

        let obj = canvasObjects[i];

        if (obj.type == "table") {
            obj.remove()
            canvas.remove(obj)
            canvas.renderAll()
        }
    }
}

function atualizarMesas() {
    $.ajax({
        type: "GET",
        url: '/Home/GetMesas/' + restaurante.codRestaurante,
        dataType: 'json',
        success: function (result) {

            var dataUltimaAtualizacaoServer = getMaxDataUltimaAlteracao(result);

            if (dataUltimaAtualizacaoServer > dataUltimaAtualizacao) {

                removerMesas();

                restaurante.mesas = result;
                restaurante.mesas.forEach(inserirMesa);

                dataUltimaAtualizacao = dataUltimaAtualizacaoServer;
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr);
            console.log(thrownError);
        }
    });
}

window.addEventListener("load", function (event) {

    restaurante.mesas.forEach(inserirMesa);
    dataUltimaAtualizacao = getMaxDataUltimaAlteracao(restaurante.mesas);

    setInterval(function () {

        atualizarMesas();
    }, 3000); // 3 seg
});
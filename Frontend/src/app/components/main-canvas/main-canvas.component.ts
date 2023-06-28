import { AfterViewChecked, AfterViewInit, Component, ElementRef, Input, QueryList, ViewChild, ViewChildren, Inject, Injectable, HostListener } from '@angular/core';
import { IConnection } from 'src/app/models/connection';
import { IScheme } from 'src/app/models/scheme';

@Component({
  selector: 'app-main-canvas',
  templateUrl: './main-canvas.component.html',
  styleUrls: ['./main-canvas.component.css']
})
export class MainCanvasComponent implements AfterViewInit, AfterViewChecked {
  @Input() scheme?: IScheme;

  @ViewChild('canvas', { static: true }) canvasRef?: ElementRef<HTMLCanvasElement>;
  @ViewChild('container', { static: true }) containerRef?: ElementRef<HTMLDivElement>;


  private canvasSize = 5000;

  private canvas!: HTMLCanvasElement;
  private context!: CanvasRenderingContext2D;

  private isDrawingLine: boolean = false;

  private createConnectionStartPoint?: { x: number, y: number };
  private createConnectionEndPoint?: { x: number, y: number };

  private connectionsLines : Map<number, {
    startPoint : { x: number, y: number }, 
    endPoint? : { x: number, y: number }
  }> = new Map();

  private connectionsLinesPoints: Map<number, Point[]> = new Map();

  creatingConnectionFromAttributeId?: number | undefined;
  creatingConnectionFromTableId? : number | undefined;

  canvasYOffset = 50;

  constructor() {}

  windowWidth = this.canvasSize;
  windowHeight = this.canvasSize;

  private scrollXOffset = 0;
  private scrollYOffset = 0;

  ngAfterViewChecked() {
    this.drawConnections();
  }

  ngAfterViewInit() {
    this.canvas = this.canvasRef!.nativeElement;
    this.adjustCanvasSize();
    this.context = this.canvas.getContext('2d')!;
  }

  adjustCanvasSize() {
    this.canvas.setAttribute('width', (this.windowWidth!).toString());
    this.canvas.setAttribute('height', (this.windowHeight!).toString());
  }

  onMouseDown(event: MouseEvent) {
    this.createConnectionStartPoint = this.getCursorPosition(event);
    console.log(this.createConnectionStartPoint);
  }

  onMouseMove(event: MouseEvent) {
    if (!this.isDrawingLine) {
      return;
    }
  
    const containerRect = this.containerRef!.nativeElement.getBoundingClientRect();
    this.createConnectionEndPoint = {
      x: event.clientX - containerRect.x,
      y: event.clientY - containerRect.y
    };

    this.draw();
  }

  onMouseUp() {
    this.isDrawingLine = false;
    this.createConnectionStartPoint = undefined;
    this.createConnectionEndPoint = undefined;
  }

  private getCursorPosition(event: MouseEvent) {
    const containerRect = this.containerRef!.nativeElement.getBoundingClientRect();
    return {
      x: event.clientX - containerRect.left,
      y: event.clientY - containerRect.top
    };
  }

  private clearCanvas() {
    this.context.clearRect(0, 0, this.canvas.width, this.canvas.height);
  }

  private drawLine(startPoint: { x : number, y : number }, endPoint: { x : number, y : number}) {
    this.context.beginPath();
    this.context.moveTo(startPoint!.x,  startPoint!.y - this.canvasYOffset);
    this.context.lineTo(endPoint!.x, endPoint!.y - this.canvasYOffset);
    this.context.stroke();
  }

  private draw() {
    this.clearCanvas();
    for (const entry of this.connectionsLinesPoints) {
      let [key, points] = entry;
      for (let i = 0; i < points.length - 1; i++) {
        this.drawLine(points[i], points[i + 1]);
      }
    }

    if (this.createConnectionStartPoint && this.createConnectionEndPoint!) {
          this.drawLine({ x: this.createConnectionStartPoint!.x, y: this.createConnectionStartPoint!.y + this.canvasYOffset },
      { x: this.createConnectionEndPoint!.x, y: this.createConnectionEndPoint!.y + this.canvasYOffset });
    }
  }

  onTableMoved(event : any) {
    this.scheme!.tables[event.index].x += event.x;
    this.scheme!.tables[event.index].y += event.y;
  }

  onCreatingConnectionFromAttributeId(event : { attributeId: number | undefined, tableId: number | undefined }) {
    if (event.attributeId) {
      this.isDrawingLine = true;
    }

    this.creatingConnectionFromAttributeId = event.attributeId;
    this.creatingConnectionFromTableId = event.tableId;
  }

  onAttributeRemoved(event: number) {
    for (let table of this.scheme!.tables) {
      for (let attribute of table.attributes) {
        attribute.connectionsTo = attribute.connectionsTo.filter(c => c.attributeToId != event);
      }
    }
    this.drawConnections();
  }

  onTableDeteled(event : number) {
    this.scheme!.tables = this.scheme?.tables.filter(t => t.id != event)!;
    this.drawConnections();
  }

  onResetConnectionCreation() {
    this.creatingConnectionFromAttributeId = undefined;
  }

  drawConnections() {
    this.connectionsLinesPoints.clear();
    this.scheme!.tables.map(
      t => t.attributes.map(
        attribute => attribute.connectionsTo.map(
          connection => {
            let elementFrom =  document.getElementById(connection.attributeFromId.toString())!;
            let elementTo =  document.getElementById(connection.attributeToId.toString())!;

            let rectFrom = elementFrom.getBoundingClientRect();
            let rectTo = elementTo.getBoundingClientRect();

            let firstPointLeft: Point = {
              x: rectFrom.x,
              y: rectFrom.height / 2 + rectFrom.y
            }
            
            let firstPointRight: Point = {
              x: rectFrom.x + rectFrom.width,
              y: firstPointLeft.y
            }

            let secondPointLeft: Point = {
              x: rectTo.x,
              y: rectTo.height / 2 + rectTo.y
            }

            let secondPointRigth: Point = {
              x: rectTo.x + rectTo.width,
              y: secondPointLeft.y
            }

            let leftToRightConnectionLength = this.calculateLineLength(firstPointLeft, secondPointRigth);
            let righToLeftConnectionLength = this.calculateLineLength(firstPointRight, secondPointLeft);
            let rightToRightConnectionLength = this.calculateLineLength(firstPointRight, secondPointRigth);
            let leftToLeftConnectionLength = this.calculateLineLength(firstPointLeft, secondPointLeft);

            let minConnectionLength = Math.min(leftToRightConnectionLength, righToLeftConnectionLength,
              rightToRightConnectionLength, leftToLeftConnectionLength);

            const outerLineLength = 40;
            let rightStartPoint: Point = {
              x: firstPointRight.x,
              y: firstPointRight.y
            }
            let leftStartPoint: Point = {
              x: firstPointLeft.x,
              y: firstPointLeft.y
            }
            let rightEndPoint: Point = {
              x: secondPointRigth.x,
              y: secondPointRigth.y
            }
            let leftEndPoint: Point = {
              x: secondPointLeft.x,
              y: secondPointLeft.y
            }
            
            switch (minConnectionLength) {
              case leftToRightConnectionLength:
                this.connectionsLinesPoints.set(connection.id, [leftStartPoint, { x: firstPointLeft.x - outerLineLength, y: firstPointLeft.y },
                  { x: secondPointRigth.x + outerLineLength, y: secondPointRigth.y }, rightEndPoint]);
                break;

              case righToLeftConnectionLength:
                
                this.connectionsLinesPoints.set(connection.id, [rightStartPoint, { x: firstPointRight.x + outerLineLength, y: firstPointRight.y }, 
                  { x: secondPointLeft.x - outerLineLength, y: secondPointLeft.y }, rightEndPoint]);
                break;

              case rightToRightConnectionLength:
                this.connectionsLinesPoints.set(connection.id, [rightStartPoint , { x: firstPointRight.x + outerLineLength, y: firstPointRight.y }, 
                  { x: secondPointRigth.x + outerLineLength, y: secondPointRigth.y }, rightEndPoint]);
                break;

              case leftToLeftConnectionLength:
                this.connectionsLinesPoints.set(connection.id, [leftStartPoint, { x: firstPointLeft.x - outerLineLength, y: firstPointLeft.y }, 
                  { x: secondPointLeft.x - outerLineLength, y: secondPointLeft.y }, leftEndPoint]);
            }
            
            let points = this.connectionsLinesPoints.get(connection.id)!;
            points.map(p => {
              p.x = p.x + this.scrollXOffset
              p.y = p.y + this.scrollYOffset
            });
          }
        )
      )
    );
    this.clearCanvas();
    this.draw();
  }

  private calculateLineLength(firstPoint: { x: number, y: number }, secondPoint: { x: number, y: number }) {
    return Math.sqrt(Math.abs(firstPoint.x - secondPoint.x)**2 + Math.abs(firstPoint.y - secondPoint.y)**2);
  }

  onConnectionCreated(event : IConnection) {
    this.scheme!.tables.find(t => t.id == this.creatingConnectionFromTableId!)!
      .attributes.find(a => a.id == event.attributeFromId)!.connectionsTo.push(event);

    console.log(this.scheme);
    this.drawConnections();
  }

  @HostListener('window:scroll')
  onScroll() {
    let rect = this.canvasRef!.nativeElement.getBoundingClientRect();
    this.scrollXOffset = window.scrollX;
    this.scrollYOffset = window.scrollY;

    this.drawConnections();
  }
}

type Point = {
  x: number;
  y: number;
};
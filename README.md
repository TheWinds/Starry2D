## What
> Starry2D是一款基于GDI+ 设计的面向对象2D游戏框架，简单且易用。

## Why
  当我们想开发一个WinForm小游戏的时候一般的做法是利用已有的WinForm控件结合多线程进行开发。如果想要更好的视觉效果的话，可以直接从Graphics上绘制界面。无论是哪种方式都糅合了游戏逻辑和视图渲染，这种代码的复用性比较低，并且不利于重构。
这时做一个简单的游戏框架的想法便应运而生，Starry2D的作用就是将游戏逻辑和视图渲染相分离，将游戏对象统一交给游戏引擎进行处理，这样开发一款游戏只要做好每个游戏对象自己的事情就OK了。
> 上面都是废话只是 (为了完成期末作业...)

## Features
 - 支持面向对象的方式进行开发
 - 多线程渲染以及优化，帧数可达60FPS
 - 提供碰撞检测接口
 - 支持声音播放
 - 支持游戏对象之间通信

## Demos
  基于Starry2D开发的Demo有：FlappyBird、打字游戏、演示程序（星空粒子效果，碰撞小球）。可以运行Demos直接查看
  
## Some Demo Gifs
![StarrySky]("http://orfg3zirg.bkt.clouddn.com/demo1.gif")

![Ball]("http://orfg3zirg.bkt.clouddn.com/demo2.gif")

![FlappyBird]("http://orfg3zirg.bkt.clouddn.com/demo3.gif")
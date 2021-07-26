# HydrocylinderPID
-------
## Design concept
### Our UI design focuses on the convenience during the process of adjusting the parameters of the PID and the user friendliness during the process of displaying the result curve and measuring. With the help of Unity and C#, we are able to create a graphical interface that enable users to adjust the PID parameters in real time, and control the whole system by software.

![image](https://user-images.githubusercontent.com/68061848/126963596-5f7f5e51-9a00-4e9c-ade6-9bd8a23e507d.png)

## Implementation platform
### Our program has a strong object-oriented characteristic, for which the controlled system can be considered as the main object that have a various property and the parameters and the scrollbars to adjust its parameters can also be regard as independent objects.
### At the same time, the need of UI interface that can be drag or scroll in real-time pushes us to choose Unity as the main platform. It gave us the start() and update() function which enabled us of realizing the real-time monitoring and real-time update.
### Considering the above two requirements, we choose Unity as the develop tool of the interface, together with control scripts written in C#.

![image](https://user-images.githubusercontent.com/68061848/126963873-a8bfa801-ab0c-4216-b2a4-abda274c45e0.png)



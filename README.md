# UnitywithROS
Unity project for simulation with ROS
## ROS2側
ROS TCP-Endpointがあるワークスペースに移動した上で
```bash
source install/setup.bash
ros2 run ros_tcp_endpoint default_server_endpoint --ros-args -p ROS_IP:=0.0.0.0
```
適宜他のノードも
## 参考
[【エディタ拡張徹底解説】初級編【Unity】](https://caitsithware.com/wordpress/archives/1377)<br>
[【Unity】【エディタ拡張】EditorWindowをモーダルやポップアップなどとして開く方法まとめ](https://light11.hatenadiary.com/entry/2022/06/01/200914)<br>
[【Unity】編集不可のパラメータをInspectorに表示する](https://kazupon.org/unity-no-edit-param-view-inspector/)<br>
[Windows上でUnityとROS2を連携させる](https://qiita.com/sfc_nakanishi_lab/items/40ab94a2f19f375f42db)

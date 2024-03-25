# Photon을 이용한 FPS 게임 (PhotonAtteck)
![img2](https://github.com/zigoom/PotonAtteck/assets/24885296/3e8743fe-b763-4e45-a2e4-16fc17d42e68)


### 1. 개발 목표  
&nbsp;&nbsp;&nbsp;   Photon을 이용한 멀티플레이 콘텐츠 제작   
<br/>
### 2. 개발환경 및 도구  
  - **소스 관리 -**  Github
  - **시용 디바이스 -**  PC  
  - **Unity -** 2019.3.15f Ver 
  - **IDE -** Visual Studio Community 2017  
<br/>

### 3. 프로젝트 특징  
&nbsp;&nbsp;&nbsp;   기존의 VR 기기에서의 디펜스 게임에서 PC 기반에 Photon PUN2로 적용하여 네트워크상에서 사용자들이 대전하는 방식으로 변경  
<br/>

### 4. 사용 기술  
  - 사용 에셋의 Collider가 아니라 BoxCollider를 사용하여 부하를 줄이고, 사용자의 이동범위를 제한
  - 프로젝트내에서 사용자가 참가하면 메인 카메라를 가지고, 본인 기준으로 다른 플레이어는 prefeb 을 생성해서 피격당하면 네트워크에서 피격사실을 전체적으로 알려서 동시에 반영되도록 설정
  

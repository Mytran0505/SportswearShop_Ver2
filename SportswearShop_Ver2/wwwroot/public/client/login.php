<!DOCTYPE html>
<html lang="vi" >
<head>
  <meta charset="UTF-8">
  <title>ITGo Shop</title>
	<!-- Favicon -->
	<link rel="icon" type="image/png" href="images/favi.png">
  <link rel='stylesheet' href='https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.14.0/css/all.min.css'><link rel="stylesheet" href="./style01.css">

</head>
<body>
<!-- partial:index.partial.php -->

<div class="container" id="container">
	<div class="form-container sign-up-container">
		<form action="#">
			<div class="logo">
				<a href="product-detail.html"><img src="images/logo.png" alt="logo" ></a>
			</div>
			<h1>Tạo tài khoản</h1>
			<input type="number" placeholder="Số điện thoại" />
			<input type="text" placeholder="Username" />
			<input type="password" placeholder="Mật khẩu" />
			<button>Đăng ký</button>
		</form>
	</div>
	<div class="form-container sign-in-container">
		<form action="#">
			<div class="logo">
				<a href="product-detail.html"><img src="images/logo.png" alt="logo"></a>
			</div>
			<h1>Đăng nhập</h1>
			<input type="email" placeholder="Email" />
			<input type="password" placeholder="Password" />
			<button>Đăng nhập</button>
		</form>
	</div>
	<div class="overlay-container">
		<div class="overlay">
			<div class="overlay-panel overlay-left">
				<h1>Mừng bạn trở lại!</h1>
				<p>Đăng nhập để tiếp tục mua sắm nhé!</p>
				<button class="ghost" id="signIn">Đăng nhập</button>
			</div>
			<div class="overlay-panel overlay-right">
				<h1>Xin chào!</h1>
				<p>Đăng ký tài khoản và trải nghiệm ngay!</p>
				<button class="ghost" id="signUp">Đăng ký</button>
			</div>
		</div>
	</div>
</div>

<footer>
	<p>
		Cảm ơn đã tin tưởng ITGo Shop! Hi vọng bạn sẽ có trải nghiệm mua hàng tốt nhất <i class="fa fa-heart"></i>
		
	</p>
</footer>
<!-- partial -->
  <script  src="./js/script.js"></script>

</body>
</html>

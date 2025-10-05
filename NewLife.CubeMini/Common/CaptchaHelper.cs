using SkiaSharp;

namespace NewLife.Cube.Common;

/// <summary>
/// 验证码帮助类
/// </summary>
public static class CaptchaHelper
{
	/// <summary>
	/// 生成验证码文本
	/// </summary>
	/// <param name="length">验证码长度，默认4位</param>
	/// <returns>验证码文本</returns>
	public static string GenerateCaptcha(int length = 4)
	{
		const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
		var random = new Random();
		var result = new char[length];
		for (var i = 0; i < length; i++)
		{
			result[i] = chars[random.Next(chars.Length)];
		}
		return new string(result);
	}

	/// <summary>
	/// 生成带噪音的验证码图片
	/// </summary>
	/// <param name="text">验证码文本</param>
	/// <param name="width">图片宽度</param>
	/// <param name="height">图片高度</param>
	/// <returns>PNG图片字节数组</returns>
	public static byte[] GenerateCaptchaImage(string text, int width = 120, int height = 40)
	{
		var info = new SKImageInfo(width, height);
		using var surface = SKSurface.Create(info);
		var canvas = surface.Canvas;

		// 清除背景
		canvas.Clear(SKColors.White);

		var random = new Random();

		// 添加背景噪点
		var noisePaint = new SKPaint
		{
			Color = SKColors.LightGray,
			StrokeWidth = 1
		};

		for (var i = 0; i < 200; i++)
		{
			var x = random.Next(width);
			var y = random.Next(height);
			canvas.DrawPoint(x, y, noisePaint);
		}

		// 添加干扰线
		var linePaint = new SKPaint
		{
			Color = SKColors.Gray,
			StrokeWidth = 1,
			IsAntialias = true
		};

		for (var i = 0; i < 5; i++)
		{
			var x1 = random.Next(width);
			var y1 = random.Next(height);
			var x2 = random.Next(width);
			var y2 = random.Next(height);
			canvas.DrawLine(x1, y1, x2, y2, linePaint);
		}

		// 绘制文字
		var textPaint = new SKPaint
		{
			Color = SKColors.Black,
			IsAntialias = true,
		};

		var textFont = new SKFont(SKTypeface.FromFamilyName("Arial"), 20)
		{
			Embolden = true // 使用 Embolden 实现加粗
		};

		// 随机颜色数组
		var colors = new[] { SKColors.Blue, SKColors.Red, SKColors.Green, SKColors.Purple, SKColors.Orange };

		float charWidth = (float)width / text.Length;
		
		for (int i = 0; i < text.Length; i++)
		{
			// 随机颜色
			textPaint.Color = colors[random.Next(colors.Length)];

			// 随机位置和角度
			var x = i * charWidth + random.Next(-5, 15);
			var y = height / 2 + random.Next(-8, 8);

			// 保存画布状态
			canvas.Save();

			// 随机旋转
			var angle = random.Next(-15, 15);
			
			canvas.RotateDegrees(angle, x, y);

			// 绘制字符
			
			canvas.DrawText(text[i].ToString(), x, y, SKTextAlign.Center, textFont, textPaint);
			
			// 恢复画布状态
			canvas.Restore();
		}

		// 添加更多干扰
		var ellipsePaint = new SKPaint
		{
			Color = SKColors.LightBlue,
			Style = SKPaintStyle.Stroke,
			StrokeWidth = 1,
			IsAntialias = true
		};

		for (var i = 0; i < 3; i++)
		{
			var x = random.Next(width);
			var y = random.Next(height);
			var w = random.Next(10, 30);
			var h = random.Next(10, 30);
			canvas.DrawOval(x, y, w, h, ellipsePaint);
		}

		// 获取图像数据
		using var image = surface.Snapshot();
		using var data = image.Encode(SKEncodedImageFormat.Png, 100);
		return data.ToArray();
	}
}

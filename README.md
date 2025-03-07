🌊 BERMUDOS

1️⃣ Giới thiệu

🛠 Bermudos là tựa game nghiêm túc đầu tiên của tôi, được phát triển sau khi hoàn thành khóa học Unity 2D Dungeon Gunner Roguelike Development Course. Đây là cách tôi áp dụng và nâng cao những kiến thức đã học.

2️⃣ Thể loại
🎮 Phiêu lưu, ARPG

3️⃣ Cốt truyện
🏝 Bermudos là một hòn đảo bí ẩn, được đồn đại có thể ban tặng bất kỳ điều ước nào. Ryu, nhân vật chính của chúng ta, là một nhà thám hiểm đang tìm kiếm bí ẩn của hòn đảo. 
Trong game, người chơi sẽ hóa thân thành Ryu, khám phá Bermudos, đối mặt với thử thách và giải mã những bí mật ẩn giấu.

🔥 4️⃣ Điểm Nhấn
4.1 🤖 AI Boss dùng Behaviour Tree

📌 Trong dự án Bermudos, tôi đã sử dụng Behavior Tree để xây dựng AI cho boss.

📌 Boss có nhiều hành vi, bao gồm:

⚔ Phản ứng với sát thương

🔄 Chuyển pha chiến đấu

🎯 Lựa chọn kỹ năng dựa trên trạng thái của người chơi

📷 Hình minh họa:

 ![Image](https://github.com/user-attachments/assets/e46d2897-f0e1-4011-b93e-f771b44193f6)

📷 Cấu trúc Behaviour Tree:

![Image](https://github.com/user-attachments/assets/175af918-f162-4e7e-ad4f-5e783c551fb9)

4.2 ⚡ Kỹ năng đặc biệt: Bash

🎮 Lấy cảm hứng từ Bash trong Ori and the Blind Forest

🛠 Tùy chỉnh lại để phù hợp hơn, biến nó thành parry tầm xa

📷 Hình minh họa:

 ![Image](https://github.com/user-attachments/assets/f2d9f1f2-bf9c-46b2-8c03-dc1f2f3e7f42)

🖋 Dùng LineRenderer để vẽ quỹ đạo:

📌 Sử dụng công thức để xác định tail của mũi tên

📷 Hình minh họa:

![Image](https://github.com/user-attachments/assets/02b18475-448b-4430-b59c-ec3f5ead7682)

📷 Code:

![Image](https://github.com/user-attachments/assets/083e807a-a6a0-4390-a125-cda4052e302c)

4.3 🎯 Hệ Thống Điều hướng đi của Ammo: AnimationCurve + Normalization

📌 Định nghĩa X & Y của Curve: Giá trị từ 0 → 1

📌 Ý tưởng: Evaluate giá trị Y * Khoảng cách từ viên đạn tới người chơi

🚨 Vấn đề xảy ra:

❌ Viên đạn không giải quyết được phần bù giữa nhân vật & viên đạn vì chúng không cùng một trục X hoặc Y

✅ Giải pháp:

📈 Thêm một Curve thứ 2 (Axis Correction) để bù trừ

📷 Hình minh họa:

![Image](https://github.com/user-attachments/assets/78babe19-8038-4936-a0a8-bfdb857ebf95)
 

5️⃣ 🎨 Các Công Cụ Hỗ Trợ
✨ Allin1Shader:

Hỗ trợ tạo hiệu ứng đồ họa như glow, dissolve, hologram dễ dàng
🔤 TMPEffect:

Hỗ trợ hiệu ứng động cho TextMeshPro
📌 Unity Version: 2022.3.44
🔗 Link game: https://quygdb.itch.io/bermudos

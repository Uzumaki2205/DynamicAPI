# DYNAMIC API TEMPLATE REPORT
## Process
- Label -> DONE
- Table -> DONE
- Chart -> DONE
- Image -> DONE
- Color -> DONE

## USING
- Tạo bảng không cần sử dụng màu
+ Json sử dụng 1 cấp nhưng tên các cấp ở những array json ở các bảng sau phải khác nhau
+ Ví dụ:
* Word: [[stt]] [[name]]
* Json
{
	"NameTable": [
		{
			"stt": "1",
			"name": "bảng 1"
		},
		{
			"stt": "1",
			"name": "bảng 1 tiếp theo"
		},
	]
	"NameTable": [
		{
			"stt_b2": "2",
			"name_b2": "bảng 2"
		},
		{
			"stt_b2": "2",
			"name_b2": "bảng 2 tiếp theo"
		},
	]
}
- Tạo bảng có sử dụng màu 
+ Màu tự động tạo tương ứng với mức độ lỗi
+ Json sử dụng 2 cấp array, có thể cùng tên ở những bảng tiếp theo
+ Ví dụ:
* Word: [[Table1.stt]] [[Table1.name]] [[Table1.color-risk]][[Table1.Color]:merge-xml]
-> Tạo màu bằng [[Tên bảng.Color]:merge-xml] -> phải có chữ color- trước tên của trường nguy hiểm
[[Table2stt]] [[Table2.name]]
* Json
{
	"NameTable": [
		{
			"Table1": [
				{
					"stt": "1",
					"name": "bảng 1"
					"color-risk": "Cao"
				},
				{
					"stt": "2",
					"name": "bảng 1 tiếp theo"
					"color-risk": "Thấp"
				},
			]
		}
	],
	"NameTable": [
		{
			"Table2": [
				{
					"stt": "1",
					"name": "bảng 2"
					"color-risk": "Cao"
				},
				{
					"stt": "2",
					"name": "bảng 2 tiếp theo"
					"color-risk": "Thấp"
				},
			]
		}
	]
}
- Tạo Chart
+ Ví dụ
* Word [[pie.Tên trường dữ liệu]] -> định nghĩa danh sách kí hiệu của biểu đồ
[[pie.value]] -> Định nghĩa biểu đồ vẽ tương ứng với lỗi được khai báo
* Json
{
	"Chart": {
		"Nguy hiểm": 4,
		"Cao": 3,
		"Trung Bình": 2,
		"Thấp": 1
	}
}

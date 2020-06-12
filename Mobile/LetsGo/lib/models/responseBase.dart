class ResponseBase<T> {

  T data;
  String message;

  ResponseBase({this.data, this.message});

  factory ResponseBase.fromJson(Map<String, dynamic> json) {
    return ResponseBase(
      message: json['message']
    );
  }
}
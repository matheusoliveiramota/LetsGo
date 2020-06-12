import 'package:geolocator/geolocator.dart';

class GeolocatorService {

  Future<Position> getPosition() async {

    GeolocationStatus geolocationStatus  = await Geolocator().checkGeolocationPermissionStatus();

    if(geolocationStatus == GeolocationStatus.denied || geolocationStatus == GeolocationStatus.disabled ||
        geolocationStatus == GeolocationStatus.unknown || geolocationStatus == GeolocationStatus.restricted)
    {
      try {
        return await Geolocator().getCurrentPosition(desiredAccuracy: LocationAccuracy.high);
      }
      catch(Exception) {
        return null;
      }
    }

    return await Geolocator().getCurrentPosition(desiredAccuracy: LocationAccuracy.high);
  }
}
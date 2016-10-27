
void main() {
	int arr[4];
	arr[0] = 7; arr[1] = 13; arr[2] = 9; arr[3] = 8;
	int sump;
	sump = 0;
	arrsum(4, arr, &sump);
	int arr2[20];
	squares(20, arr2);
	sump = 0;
	arrsum(20, arr2, &sump);
	int arr3[7];
	int freq[4];
	arr3[0] = 1; arr3[1] = 2; arr3[2] = 1; arr3[3] = 1; arr3[4] = 1; arr3[5] = 2; arr3[6] = 0;
	histogram(7, arr3, 4, freq);
	int i;
	i = 0;
	while(i < 4){
		print freq[i];
		i = i + 1;
	}
	println;
}
//Arrsum function ex 7.2.1
void arrsum(int n, int arr[], int *sump) {
	int i;
	// 7.3 Updated for for loop syntax
	for (i = 0; i < n; ++i) {
		*sump = *sump + arr[i];
	}
	print *sump;
	println;
}
//Square function, ex 7.2.2
void squares(int n, int arr[]) {
	int i;
	// 7.3 Updated for for loop syntax
	for (i = 0; i < n; ++i) {
		arr[i] = i*i;
	}
}
//Histogram function, ex 7.2.3
void histogram(int n, int ns[], int max, int freq[]) {
	int i;
	int num;
	// 7.3 Updated for for loop syntax
	for (i = 0; i < max; ++i) {
		freq[i] = 0;
	}
	// 7.3 Updated for for loop syntax
	for (i = 0; i < n; ++i) {
		num = ns[i];
		freq[num] = freq[num] + 1;
	}
}